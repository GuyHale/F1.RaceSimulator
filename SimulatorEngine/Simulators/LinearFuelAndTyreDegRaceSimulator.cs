using SimulatorEngine.Car;
using SimulatorEngine.Car.Components;
using SimulatorEngine.Car.Evolution;
using SimulatorEngine.Events;
using SimulatorEngine.Strategy;

namespace SimulatorEngine.Simulators;

public class LinearFuelAndTyreDegRaceSimulator
{
    private LapCompletedEventHandler _lapCompletedEvent = new();
    private PitstopEventHandler _pitstopEventEvent = new();

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
        
        var weatherBaseLapTimeComponent = new DryWeatherLapTimeComponent();
        var tyreDegLapTimeComponent = new LinearTyreDegradation(tyreStrengthConfigs);
        var fuelTimeLapTimeComponent = new LinearFuelLoad(timeLossPerFuelKg);
        
        IEnumerable<ILapTimeComponent> timeLossCalculators = [weatherBaseLapTimeComponent, tyreDegLapTimeComponent, fuelTimeLapTimeComponent];
        
        RaceCar car = new(startStrategy, fuelLossPerLapPerKg, baseLapTime);
        
        LapCompletedEventHandler lapCompletedEventHandler = new();
        LapSimulationEventHandler lapSimulationEventHandler = new(timeLossCalculators);
        PitstopEventHandler pitstopEventHandler = new();
        
        SimulationResultBuilder resultBuilder = new(raceStrategy);
        
        int totalLaps = 72;
        int lapNumber = 1;
        
        TimeSpan raceDuration = TimeSpan.Zero;
        Race race = new();

        try
        {
            while (lapNumber <= totalLaps)
            {
                Lap lap = new(car, lapNumber);
                
                race.AddLap(lap);
                
                LapSimulationEvent lapSimulation = new(lap);
                
                lapSimulationEventHandler.Handle(lapSimulation);

                if (raceStrategy.ShouldPerformPitstop(lapNumber, out Pitstop? pitstop))
                {
                    PitstopEvent pitstopEvent = new(lap, pitstop);
                    pitstopEventHandler.Handle(pitstopEvent);
                }

                LapCompletedEvent lapCompletedEvent = new(lap);
                
                lapCompletedEventHandler.Handle(lapCompletedEvent);

                resultBuilder.WithLap(lap);
                
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