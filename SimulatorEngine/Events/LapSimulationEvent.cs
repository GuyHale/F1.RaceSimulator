namespace SimulatorEngine.Events;

public record LapSimulationEvent(Lap Lap)
    : IRaceEvent;