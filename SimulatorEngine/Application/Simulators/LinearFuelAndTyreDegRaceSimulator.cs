using SimulatorEngine.Domain.Car;
using SimulatorEngine.Domain.Car.Evolution;
using SimulatorEngine.Domain.Events;
using SimulatorEngine.Domain.Race;
using SimulatorEngine.Domain.Simulation;

namespace SimulatorEngine.Application.Simulators;

public class LinearFuelAndTyreDegRaceSimulator 
    : IRaceSimulation
{
    public SimulationResult Run(RaceConfiguration raceConfiguration)
    {
        var weatherBaseLapTimeComponent = new DryWeatherLapTimeComponent();
        var tyreDegLapTimeComponent = new LinearTyreDegradation(raceConfiguration.CircuitStatistics.TyreStatistics);
        var fuelTimeLapTimeComponent = new LinearFuelLoad(raceConfiguration.CircuitStatistics.FuelStatistics.TimeLossPerKg);
        
        IEnumerable<ILapTimeComponent> timeLossCalculators = [weatherBaseLapTimeComponent, tyreDegLapTimeComponent, fuelTimeLapTimeComponent];
        
        RaceCar car = new(raceConfiguration);
        
        LapCompletedEventHandler lapCompletedEventHandler = new();
        LapSimulationEventHandler lapSimulationEventHandler = new(timeLossCalculators);
        PitstopEventHandler pitstopEventHandler = new();
        
        SimulationResultBuilder resultBuilder = new(raceConfiguration.Strategy);
        
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

                if (raceConfiguration.Strategy.ShouldPerformPitstop(lapNumber, out Pitstop? pitstop))
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