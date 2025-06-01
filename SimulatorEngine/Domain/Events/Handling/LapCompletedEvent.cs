using SimulatorEngine.Domain.Race;

namespace SimulatorEngine.Domain.Events.Handling;

public readonly record struct LapCompletedEvent(Lap Lap)
    : IRaceEvent;