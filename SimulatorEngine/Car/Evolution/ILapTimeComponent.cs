namespace SimulatorEngine.Car.Evolution;

public interface ILapTimeComponent
{
    public TimeSpan Calculate(RaceCar raceCar);
}