using SimulatorEngine.Domain.Race;
using SimulatorEngine.Domain.Strategy;

namespace SimulatorEngine.Domain.Simulation;

public record SimulationResult : IComparable<SimulationResult>
{
    public TimeSpan RaceDuration { get; init; }
    public required RaceStrategy Strategy { get; init; }
    public required RaceProgress RaceProgress { get; init; }
    public bool Completed { get; init; }
    public int LapNumber { get; init; }
    public string Errors { get; init; } = string.Empty;

    public int CompareTo(SimulationResult? other)
    {
        if (other is null)
        {
            return -1;
        }

        if (Completed)
        {
            if (!other.Completed)
            {
                return -1;
            }
            
            return this.RaceDuration.CompareTo(other.RaceDuration);
        }

        if (!other.Completed)
        {
            return 0;
        }

        return 1;
    }
}