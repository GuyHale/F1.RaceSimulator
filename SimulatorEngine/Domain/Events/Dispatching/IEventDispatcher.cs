namespace SimulatorEngine.Domain.Events.Dispatching;

public interface IEventDispatcher<TEvent> 
    where TEvent : IRaceEvent
{
    void Dispatch(TEvent eventInstance);
    void RegisterHandler(IRaceEventHandler<TEvent> handler);
    void UnregisterHandler(IRaceEventHandler<TEvent> handler);
}