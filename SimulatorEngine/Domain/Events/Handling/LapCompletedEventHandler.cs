namespace SimulatorEngine.Domain.Events.Handling;

public class LapCompletedEventHandler 
    : IRaceEventHandler<LapCompletedEvent>
{
    public void Handle(LapCompletedEvent e) 
    {
        e.Lap.Car.Engine.ConsumeFuel();

        if (!e.Lap.EventOccurred(LapEventType.Pitstop))
        {
            e.Lap.Car.Tyres.IncrementWear();
        }
    }
}
