namespace SimulatorEngine.Car.Evolution;

public class LinearFuelEffectOnLapTime 
    : ITimeLossCalculator
{
    private readonly TimeSpan _timeLossPerKgFuel;

    public LinearFuelEffectOnLapTime(TimeSpan timeLossPerKgFuel)
    {
        _timeLossPerKgFuel = timeLossPerKgFuel;
    }
    
    public TimeSpan Calculate(RaceCar raceCar)
    {
        return _timeLossPerKgFuel.Multiply(raceCar.Engine.FuelKg);
    }
}