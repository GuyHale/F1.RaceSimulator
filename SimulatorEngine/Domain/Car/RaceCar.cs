using SimulatorEngine.Domain.Car.Components;
using SimulatorEngine.Domain.Events;
using SimulatorEngine.Domain.Events.Handling;
using SimulatorEngine.Domain.Race;

namespace SimulatorEngine.Domain.Car;

public record RaceCar 
{
    public RaceCar(RaceConfiguration raceConfiguration)
    {
        Engine = new Engine(raceConfiguration.Strategy.StartStrategy.StartingFuelKg, raceConfiguration.CircuitStatistics.FuelStatistics.LossPerLapKg);
        Tyres = new RaceCarTyres(raceConfiguration.Strategy.StartStrategy);
        BaseLapTime = raceConfiguration.CircuitStatistics.BaseLapTime;
    }

    public Engine Engine { get; }
    public RaceCarTyres Tyres { get; }
    public TimeSpan BaseLapTime { get; set; }

    public void ChangeTyres(Pitstop pitstop)
    {
        Tyres.ChangeTyres(pitstop);
    }
}