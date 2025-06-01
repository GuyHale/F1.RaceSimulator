using SimulatorEngine.Domain.Race;

namespace SimulatorEngine.Domain.Events;

public readonly record struct LapCompletedEvent(Lap Lap)
    : IRaceEvent;