using SimulatorEngine.Car.Components;
using SimulatorEngine.Events;
using SimulatorEngine.Strategy;

namespace SimulatorEngine.Car;

public record RaceCar 
{
    public RaceCar(RaceStartStrategy raceStartStrategy, 
        float fuelLossPerLapPerKg,
        TimeSpan baseLapTime)
    {
        Engine = new Engine(raceStartStrategy.StartingFuelKg, fuelLossPerLapPerKg);
        Tyres = new RaceCarTyres(raceStartStrategy);
        BaseLapTime = baseLapTime;
    }

    public Engine Engine { get; }
    public RaceCarTyres Tyres { get; }
    public TimeSpan BaseLapTime { get; set; }

    public void ChangeTyres(Pitstop pitstop)
    {
        Tyres.ChangeTyres(pitstop);
    }
}