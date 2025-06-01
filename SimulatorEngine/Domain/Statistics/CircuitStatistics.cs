namespace SimulatorEngine.Domain.Statistics;

public record CircuitStatistics
{
    public CircuitStatistics(TimeSpan baseLapTime, 
        ref FuelStatistics fuelStatistics,
        TyreStatistics[] tyreStatistics)
    {
        BaseLapTime = baseLapTime;
        FuelStatistics = fuelStatistics;
        TyreStatistics = tyreStatistics;
    }
    
    public TimeSpan BaseLapTime { get; }
    public FuelStatistics FuelStatistics { get; }
    public TyreStatistics[] TyreStatistics { get; }
    
}