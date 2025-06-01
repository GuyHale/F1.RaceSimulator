using SimulatorEngine.Car;

namespace SimulatorEngine.Events;

public readonly record struct LapCompletedEvent(Lap Lap)
    : IRaceEvent;