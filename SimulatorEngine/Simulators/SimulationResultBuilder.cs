using SimulatorEngine.Events;
using SimulatorEngine.Strategy;

namespace SimulatorEngine.Simulators;

public class SimulationResultBuilder
    : IDisposable
{
    private TimeSpan _raceDuration = TimeSpan.Zero;
    private readonly RaceStrategy _raceStrategy;
    private string _errors = string.Empty;
    private bool _completed = false;
    private int _lapNumber = 1;

    private LapCompletedEventHandler _lapCompletedEventHandler;
    private bool _disposed;

    public SimulationResultBuilder(RaceStrategy raceStrategy, LapCompletedEventHandler lapCompletedEventHandler)
    {
        _raceStrategy = raceStrategy;
        _lapCompletedEventHandler = lapCompletedEventHandler;
        
        SubscribeToEventsOfInterest();
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        _disposed = true;
        UnSubscribeFromEventsOfInterest();
        GC.SuppressFinalize(this);
    }

    public SimulationResultBuilder WithErrors(string errors)
    {
        _errors = errors;
        _completed = false;
        return this;
    }

    public SimulationResultBuilder WithLapTime(TimeSpan lapTime)
    {
        _raceDuration += lapTime;
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
    
    private void SubscribeToEventsOfInterest()
    {
        _lapCompletedEventHandler += OnLapCompleted;
    }
    
    private void UnSubscribeFromEventsOfInterest()
    {
        _lapCompletedEventHandler -= OnLapCompleted;
    }
    private void OnLapCompleted()
    {
        _lapNumber++;
    }
    
}