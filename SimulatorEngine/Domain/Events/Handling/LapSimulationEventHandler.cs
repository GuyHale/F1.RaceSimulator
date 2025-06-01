using SimulatorEngine.Domain.Car.Evolution;

namespace SimulatorEngine.Domain.Events.Handling;

public class LapSimulationEventHandler 
    : IRaceEventHandler<LapSimulationEvent>
{
    private readonly IEnumerable<ILapTimeComponent> _lapTimeComponents;

    public LapSimulationEventHandler(IEnumerable<ILapTimeComponent> lapTimeComponents)
    {
        _lapTimeComponents = lapTimeComponents;
    }
    
    public void Handle(LapSimulationEvent raceEvent)
    {
        foreach (var component in _lapTimeComponents)
        {
            TimeSpan lapTimeComponent = component.Calculate(raceEvent.Lap.Car);
            raceEvent.Lap.AddLapTime(lapTimeComponent);
        }
    }
}