namespace SimulatorEngine.Domain.Events.Dispatching;

public class EventDispatcher<TEvent> 
    : IEventDispatcher<TEvent> where TEvent: IRaceEvent
{
    private readonly HashSet<IRaceEventHandler<TEvent>> _handlers = [];

    public void RegisterHandler(IRaceEventHandler<TEvent> handler)
    {
        _handlers.Add(handler);
    }

    public void UnregisterHandler(IRaceEventHandler<TEvent> handler)
    {
        _handlers.Remove(handler);
    }

    public void Dispatch(TEvent eventInstance)
    {
        foreach (var handler in _handlers)
        {
            handler.Handle(eventInstance);
        }
    }
}