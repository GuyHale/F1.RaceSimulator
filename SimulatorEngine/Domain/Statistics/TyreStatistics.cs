using SimulatorEngine.Domain.Car.Components.Tyres;

namespace SimulatorEngine.Domain.Statistics;

public record TyreStatistics(TyreCompoundType Type, TimeSpan LapTimePenalty, TimeSpan DegradationPerLap);