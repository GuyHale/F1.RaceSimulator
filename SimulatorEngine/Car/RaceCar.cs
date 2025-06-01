using SimulatorEngine.Car.Components;
using SimulatorEngine.Events;
using SimulatorEngine.Strategy;

namespace SimulatorEngine.Car;

public record RaceCar 
    : IDisposable
{
    public RaceCar(RaceStartStrategy raceStartStrategy, float fuelLossPerLapPerKg, 
        LapCompletedEventHandler lapCompletedHandler,
        PitstopHandler pitstopHandler)
    {
        Engine = new Engine(raceStartStrategy.StartingFuelKg, fuelLossPerLapPerKg, lapCompletedHandler);
        Tyres = new RaceCarTyres(raceStartStrategy, lapCompletedHandler, pitstopHandler);
    }

    private bool _disposed;

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        _disposed = true;
        
        Engine.Dispose();
        Tyres.Dispose();
        
        GC.SuppressFinalize(this);
    }
    
    public Engine Engine { get; }
    public RaceCarTyres Tyres { get; }

    
}