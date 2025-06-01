namespace SimulatorEngine.Car.Evolution;

public class LinearFuelLoad 
    : ILapTimeComponent
{
    private readonly TimeSpan _timeLossPerKgFuel;

    public LinearFuelLoad(TimeSpan timeLossPerKgFuel)
    {
        _timeLossPerKgFuel = timeLossPerKgFuel;
    }
    
    public TimeSpan Calculate(RaceCar raceCar)
    {
        return _timeLossPerKgFuel.Multiply(raceCar.Engine.FuelKg);
    }
}