using SimulatorEngine.Domain.Car;
using SimulatorEngine.Domain.Events;
using SimulatorEngine.Domain.Events.Handling;

namespace SimulatorEngine.Domain.Race;

public class Lap
{
    public Lap(RaceCar car, int number)
    {
        Number = number;
        Car = car;
    }
    
    public int Number { get; }
    public RaceCar Car { get; }
    
    public TimeSpan Time { get; private set; } = TimeSpan.Zero;
    
    public LapEventType Events { get; private set; } = LapEventType.None;
    
    public void AddLapTime(TimeSpan time)
    {
        Time += time;
    }

    public void PerformPitstop(Pitstop pitstop)
    {
        Car.ChangeTyres(pitstop);
        
        AddLapTime(pitstop.Duration);
        
        Events |= LapEventType.Pitstop;
    }
    
    public bool EventOccurred(LapEventType eventType)
    {
        return Events.HasFlag(eventType);
    }
}