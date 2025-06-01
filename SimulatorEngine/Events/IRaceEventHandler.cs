namespace SimulatorEngine.Events;

public interface IRaceEventHandler<in TRaceEvent> where TRaceEvent : IRaceEvent 
{
    void Handle(TRaceEvent raceEvent);
}
