using SimulatorEngine.Events;
using SimulatorEngine.Strategy;

namespace SimulatorEngine.Car.Components;

public record RaceCarTyres : IDisposable
{
    public RaceCarTyres(RaceStartStrategy startStrategy,
        LapCompletedEventHandler lapCompletedEventHandler,
        PitstopHandler pitstopEventHandler)
    {
        TyreCompoundType tyreCompoundType = startStrategy.TyreCompound;
        
        FrontLeft = new Tyre(tyreCompoundType, startStrategy.TyreAges.FrontLeftAge, lapCompletedEventHandler);
        FrontRight = new Tyre(tyreCompoundType, startStrategy.TyreAges.FrontRightAge, lapCompletedEventHandler);
        RearLeft = new Tyre(tyreCompoundType, startStrategy.TyreAges.RearLeftAge, lapCompletedEventHandler);
        RearRight = new Tyre(tyreCompoundType, startStrategy.TyreAges.RearRightAge, lapCompletedEventHandler);
        
        _pitstopHandler = pitstopEventHandler;

        SubscribeToEventsOfInterest();
    }
    
    private PitstopHandler _pitstopHandler;
    private bool _disposed;

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        _disposed = true;
        
        FrontLeft.Dispose();
        FrontRight.Dispose();
        RearLeft.Dispose();
        RearRight.Dispose();

        UnSubscribeFromEventsOfInterest();
        
        GC.SuppressFinalize(this);
    }
    
    public Tyre FrontLeft { get; }
    public Tyre FrontRight { get; }
    public Tyre RearLeft { get; }
    public Tyre RearRight { get; }
    
    private void SubscribeToEventsOfInterest()
    {
        _pitstopHandler += OnPitstop;
    }
    
    private void UnSubscribeFromEventsOfInterest()
    {
        _pitstopHandler -= OnPitstop;
    }
    
    private void OnPitstop(Pitstop pitstop)
    {
        FrontLeft.OnPitStop(pitstop.TyreCompound, pitstop.TyreAges.FrontLeftAge);
        FrontRight.OnPitStop(pitstop.TyreCompound, pitstop.TyreAges.FrontRightAge);
        RearLeft.OnPitStop(pitstop.TyreCompound, pitstop.TyreAges.RearLeftAge);
        RearRight.OnPitStop(pitstop.TyreCompound, pitstop.TyreAges.RearRightAge);
    }
}