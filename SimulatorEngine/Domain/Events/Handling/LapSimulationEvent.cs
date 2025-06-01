using SimulatorEngine.Domain.Race;

namespace SimulatorEngine.Domain.Events.Handling;

public record LapSimulationEvent(Lap Lap)
    : IRaceEvent;