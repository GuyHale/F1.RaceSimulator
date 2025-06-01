using SimulatorEngine.Domain.Statistics;
using SimulatorEngine.Domain.Strategy;

namespace SimulatorEngine.Domain.Race;

public record RaceConfiguration(RaceStrategy Strategy, CircuitStatistics CircuitStatistics);