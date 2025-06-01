namespace SimulatorEngine.Car.Evolution;

public class DryWeatherLapTimeComponent : ILapTimeComponent
{
    public TimeSpan Calculate(RaceCar car) => car.BaseLapTime;
}