using SimulatorEngine.Domain.Race;

namespace SimulatorEngine.Domain.Events;

public record LapSimulationEvent(Lap Lap)
    : IRaceEvent;