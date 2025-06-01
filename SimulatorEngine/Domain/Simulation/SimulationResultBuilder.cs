using SimulatorEngine.Domain.Race;
using SimulatorEngine.Domain.Strategy;

namespace SimulatorEngine.Domain.Simulation;

public class SimulationResultBuilder
{
    private TimeSpan _raceDuration = TimeSpan.Zero;
    private readonly RaceStrategy _raceStrategy;
    private string _errors = string.Empty;
    private bool _completed = false;
    private int _lapNumber = 1;

    public SimulationResultBuilder(RaceStrategy raceStrategy)
    {
        _raceStrategy = raceStrategy;
    }

    public SimulationResultBuilder WithErrors(string errors)
    {
        _errors = errors;
        _completed = false;
        return this;
    }

    public SimulationResultBuilder WithLap(Lap lap)
    {
        _raceDuration += lap.Time;
        return this;
    }

    public SimulationResultBuilder WithCompleted()
    {
        _completed = true;
        return this;
    }
    
    public SimulationResult Build()
    {
        return new SimulationResult()
        {
            RaceDuration = _raceDuration,
            Strategy = _raceStrategy,
            Completed = _completed,
            Errors = _errors,
            LapNumber = _lapNumber
        };
    }

    public void OnLapCompleted()
    {
        _lapNumber++;
    }
    
}