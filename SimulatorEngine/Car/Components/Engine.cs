using SimulatorEngine.Events;

namespace SimulatorEngine.Car.Components;

public class Engine
    : IDisposable
{
    public Engine(float initialFuelKg, float fuelLossPerLapKg, LapCompletedEventHandler lapCompletedEventHandler)
    {
        FuelKg = initialFuelKg;
        FuelLossPerLapKg = fuelLossPerLapKg;
        
        _lapCompletedEventHandler = lapCompletedEventHandler;
        SubscribeToEventsOfInterest();
    }
    
    private LapCompletedEventHandler _lapCompletedEventHandler;
    private bool _disposed;

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
    
    public float FuelKg { get; private set; }
    private float FuelLossPerLapKg { get; }
    
    private void OnLapCompleted()
    {
        FuelKg -= FuelLossPerLapKg;

        if (FuelKg <= 0)
        {
            throw new Exception("Fuel in the engine must be greater than 0");
        }
    }
    
    private void SubscribeToEventsOfInterest()
    {
        _lapCompletedEventHandler += OnLapCompleted;
    }
    
    private void UnSubscribeFromEventsOfInterest()
    {
        _lapCompletedEventHandler -= OnLapCompleted;
    }
}