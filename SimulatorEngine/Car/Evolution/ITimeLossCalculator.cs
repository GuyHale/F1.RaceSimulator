namespace SimulatorEngine.Car.Evolution;

public interface ITimeLossCalculator
{
    public TimeSpan Calculate(RaceCar raceCar);
}