using SimulatorEngine.Domain.Race;

namespace SimulatorEngine.Domain.Events;

public record PitstopEvent(Lap Lap, Pitstop Pitstop) : IRaceEvent;