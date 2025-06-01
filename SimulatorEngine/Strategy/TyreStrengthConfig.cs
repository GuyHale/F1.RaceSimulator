using SimulatorEngine.Car.Components;

namespace SimulatorEngine.Strategy;

public record TyreStrengthConfig(TyreCompoundType Type, TimeSpan LapTimePenalty, TimeSpan DegradationPerLap);