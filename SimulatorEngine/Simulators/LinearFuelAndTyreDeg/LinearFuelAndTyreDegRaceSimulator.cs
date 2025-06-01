using SimulatorEngine.Car;
using SimulatorEngine.Car.Components;
using SimulatorEngine.Car.Evolution;
using SimulatorEngine.Events;
using SimulatorEngine.Strategy;

namespace SimulatorEngine.Simulators.LinearFuelAndTyreDeg;

public class LinearFuelAndTyreDegRaceSimulator
{
    private LapCompletedEventHandler _lapCompletedEvent = new();
    private PitstopHandler _pitstopEvent = new();

    public SimulationResult Run()
    {
        RaceStartStrategy startStrategy = new(StartingFuelKg: 100, TyreCompoundType.Soft, new TyreAges(0));
        
        List<Pitstop> pitstops = [new Pitstop(18, TimeSpan.FromSeconds(24), TyreCompoundType.Hard, new TyreAges(0))];
        
        RaceStrategy raceStrategy = new RaceStrategy(startStrategy, pitstops);

        TyreStrengthConfig[] tyreStrengthConfigs =
        [
            new(TyreCompoundType.Soft, TimeSpan.Zero, TimeSpan.FromMilliseconds(200)),
            new(TyreCompoundType.Medium, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(100)),
            new(TyreCompoundType.Hard, TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(65)),
        ];
        
        TimeSpan timeLossPerFuelKg = TimeSpan.FromMilliseconds(30);
        TimeSpan baseLapTime = TimeSpan.FromSeconds(97.5);
        
        float fuelLossPerLapPerKg = 1.5f;
        
        var tyreDegCalculator = new LinearTyreDegradation(tyreStrengthConfigs);
        var fuelTimeLossCalculator = new LinearFuelEffectOnLapTime(timeLossPerFuelKg);
        
        IEnumerable<ITimeLossCalculator> timeLossCalculators = [tyreDegCalculator, fuelTimeLossCalculator];
        
        using RaceCar car = new(startStrategy, fuelLossPerLapPerKg, _lapCompletedEvent, _pitstopEvent);
        using LinearFuelAndTyreDegLapSimulator lapSimulator = new(timeLossCalculators, _lapCompletedEvent,  _pitstopEvent);
        using SimulationResultBuilder resultBuilder = new(raceStrategy, _lapCompletedEvent);
        
        int totalLaps = 72;
        int lapNumber = 1;
        
        TimeSpan raceDuration = TimeSpan.Zero;

        try
        {
            while (lapNumber <= totalLaps)
            {
                lapSimulator.SimulateLap(baseLapTime, car);

                var pitStop = pitstops.FirstOrDefault(p => p.Lap == lapNumber);

                if (pitStop is not null)
                {
                    _pitstopEvent.Invoke(pitStop);
                }

                TimeSpan lapTime = lapSimulator.Length;
                raceDuration += lapTime;
                
                resultBuilder.WithLapTime(lapTime);
                
                _lapCompletedEvent.Invoke();
                lapNumber++;
            }
        }
        catch (Exception ex)
        {
            resultBuilder.WithErrors(ex.Message);
        }

        if (lapNumber > totalLaps)
        {
            resultBuilder.WithCompleted();
        }

        return resultBuilder.Build();
    }
}