using SimulatorEngine.Events;

namespace SimulatorEngine.Strategy;

public record RaceStrategy
{
    public RaceStrategy(RaceStartStrategy raceStartStrategy, List<Pitstop> pitstops)
    {
        StartStrategy = raceStartStrategy;
        Pitstops = pitstops;
    }
    
    public RaceStartStrategy StartStrategy { get; }
    public List<Pitstop> Pitstops { get; } = [];
}