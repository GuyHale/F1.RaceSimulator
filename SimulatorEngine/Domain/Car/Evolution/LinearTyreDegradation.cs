using SimulatorEngine.Domain.Car.Components.Tyres;
using SimulatorEngine.Domain.Statistics;

namespace SimulatorEngine.Domain.Car.Evolution;

public record LinearTyreDegradation(TyreStatistics[] TyreStrengthConfigs)
    : ILapTimeComponent
{
    public TimeSpan Calculate(RaceCar raceCar)
    {
        return TimeSpan.Zero
            + CalculateTimeLossForTyre(raceCar.Tyres.FrontLeft)
            + CalculateTimeLossForTyre(raceCar.Tyres.FrontRight)
            + CalculateTimeLossForTyre(raceCar.Tyres.RearLeft)
            + CalculateTimeLossForTyre(raceCar.Tyres.RearRight);
    }

    private TimeSpan CalculateTimeLossForTyre(Tyre tyre)
    {
        var tyreStrengthConfig = TyreStrengthConfigs.FirstOrDefault(x => x.Type == tyre.Type)
                                 ?? throw new Exception($"No TyreStrengthConfig with type {tyre.Type} found");

        var degradationTimeLoss = tyreStrengthConfig.DegradationPerLap.Multiply(tyre.Age);
        
        return tyreStrengthConfig.LapTimePenalty.Add(degradationTimeLoss);
    }
}