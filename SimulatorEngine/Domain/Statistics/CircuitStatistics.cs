namespace SimulatorEngine.Domain.Statistics;

public record CircuitStatistics
{
    public CircuitStatistics(int totalLaps,
        TimeSpan baseLapTime, 
        ref FuelStatistics fuelStatistics,
        TyreStatistics[] tyreStatistics)
    {
        TotalLaps = totalLaps;
        BaseLapTime = baseLapTime;
        FuelStatistics = fuelStatistics;
        TyreStatistics = tyreStatistics;
    }

    public int TotalLaps { get; }
    public TimeSpan BaseLapTime { get; }
    public FuelStatistics FuelStatistics { get; }
    public TyreStatistics[] TyreStatistics { get; }
    
}