using SimulatorEngine.Events;

namespace SimulatorEngine.Car.Components;

public class Tyre : IDisposable
{
    public Tyre(TyreCompoundType type, float age, 
        LapCompletedEventHandler lapCompletedHandler)
    {
        Type = type;
        Age = age;
        
        _lapCompletedEventHandler = lapCompletedHandler;
        SubscribeToEventsOfInterest();
    }
    
    private bool _disposed = false;
    
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
    
    private LapCompletedEventHandler _lapCompletedEventHandler;
    
    public TyreCompoundType Type { get; private set; }
    public float Age { get; private set; }
    
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
        Age ++;
    }

    public void OnPitStop(TyreCompoundType type, float age)
    {
        Age = age;
        Type = type;
    }
}