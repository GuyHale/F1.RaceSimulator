using SimulatorEngine.Car;
using SimulatorEngine.Car.Evolution;
using SimulatorEngine.Events;
using SimulatorEngine.Strategy;

namespace SimulatorEngine.Simulators.LinearFuelAndTyreDeg;

public class LinearFuelAndTyreDegLapSimulator
    : IDisposable
{
    private readonly IEnumerable<ITimeLossCalculator> _timeLossCalculators;
    private LapCompletedEventHandler _lapCompletedHandler;
    private PitstopHandler _pitstopHandler;

    private bool _disposed;

    public LinearFuelAndTyreDegLapSimulator(IEnumerable<ITimeLossCalculator> timeLossCalculators, 
        LapCompletedEventHandler lapCompletedHandler,
        PitstopHandler pitstopHandler)
    {
        _timeLossCalculators = timeLossCalculators;
        
        _lapCompletedHandler = lapCompletedHandler;
        _pitstopHandler = pitstopHandler;

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
    
    public TimeSpan Length { get; private set; } = TimeSpan.Zero;
    public int Number { get; private set; } = 1;
    
    public void SimulateLap(TimeSpan baseLapTime, RaceCar raceCar)
    {
        Length += baseLapTime;

        foreach (var timeLoss in _timeLossCalculators)
        {
            Length += timeLoss.Calculate(raceCar);
        }
    }
    
    private void SubscribeToEventsOfInterest()
    {
        _lapCompletedHandler += OnLapCompleted;
        _pitstopHandler += OnPitStop;
    }
    
    private void UnSubscribeFromEventsOfInterest()
    {
        _lapCompletedHandler -= OnLapCompleted;
        _pitstopHandler -= OnPitStop;
    }
    
    private void OnPitStop(Pitstop context)
    {
        Length += context.Duration;
    }

    private void OnLapCompleted()
    {
        Length = TimeSpan.Zero;
        Number++;
    }
}