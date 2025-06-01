using SimulatorEngine.Domain.Race;

namespace SimulatorEngine.Domain.Events.Handling;

public record PitstopEvent(Lap Lap, Pitstop Pitstop) : IRaceEvent;