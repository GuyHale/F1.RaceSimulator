using System.Diagnostics.CodeAnalysis;
using SimulatorEngine.Domain.Events;
using SimulatorEngine.Domain.Events.Handling;

namespace SimulatorEngine.Domain.Strategy;

public record RaceStrategy
{
    public RaceStrategy(RaceStartStrategy raceStartStrategy, List<Pitstop> pitstops)
    {
        StartStrategy = raceStartStrategy;
        Pitstops = pitstops;
    }
    
    public RaceStartStrategy StartStrategy { get; }
    public List<Pitstop> Pitstops { get; } = [];

    public bool ShouldPerformPitstop(int lapNumber, [NotNullWhen(true)] out Pitstop? pitstop)
    {
        pitstop = Pitstops.FirstOrDefault(p => p.Lap == lapNumber);
        return pitstop is not null;
    }
}