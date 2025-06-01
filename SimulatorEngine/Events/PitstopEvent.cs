using SimulatorEngine.Car;

namespace SimulatorEngine.Events;

public record PitstopEvent(Lap Lap, Pitstop Pitstop) : IRaceEvent;