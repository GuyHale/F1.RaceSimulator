using SimulatorEngine.Domain.Car;
using SimulatorEngine.Domain.Car.Evolution;
using SimulatorEngine.Domain.Events;
using SimulatorEngine.Domain.Events.Dispatching;
using SimulatorEngine.Domain.Events.Handling;
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

        EventDispatcher<LapSimulationEvent> lapSimulationDispatcher = new();
        lapSimulationDispatcher.RegisterHandler(lapSimulationEventHandler);
        
        EventDispatcher<PitstopEvent> pitStopDispatcher = new();
        pitStopDispatcher.RegisterHandler(pitstopEventHandler);
        
        EventDispatcher<LapCompletedEvent> lapCompletionDispatcher = new();
        lapCompletionDispatcher.RegisterHandler(lapCompletedEventHandler);
        
        RaceProgress raceProgress = new();
        SimulationResultBuilder resultBuilder = new(raceProgress, raceConfiguration.Strategy);
        
        int totalLaps = raceConfiguration.CircuitStatistics.TotalLaps;
        int lapNumber = 1;
        
        TimeSpan raceDuration = TimeSpan.Zero;

        try
        {
            while (lapNumber <= totalLaps)
            {
                Lap lap = new(car, lapNumber);
                
                raceProgress.Laps.Add(lap);
                
                LapSimulationEvent lapSimulation = new(lap);
                
                lapSimulationDispatcher.Dispatch(lapSimulation);

                if (raceConfiguration.Strategy.ShouldPerformPitstop(lapNumber, out Pitstop? pitstop))
                {
                    PitstopEvent pitstopEvent = new(lap, pitstop);
                    pitStopDispatcher.Dispatch(pitstopEvent);
                }

                LapCompletedEvent lapCompletedEvent = new(lap);
                
                lapCompletionDispatcher.Dispatch(lapCompletedEvent);

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