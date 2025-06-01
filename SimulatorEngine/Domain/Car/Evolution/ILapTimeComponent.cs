namespace SimulatorEngine.Domain.Car.Evolution;

public interface ILapTimeComponent
{
    public TimeSpan Calculate(RaceCar raceCar);
}