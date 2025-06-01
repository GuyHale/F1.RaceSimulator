using SimulatorEngine.Application.Simulators;
using SimulatorEngine.Domain.Car.Components.Tyres;
using SimulatorEngine.Domain.Events.Handling;
using SimulatorEngine.Domain.Race;
using SimulatorEngine.Domain.Statistics;
using SimulatorEngine.Domain.Strategy;

RaceStartStrategy startStrategy = new(StartingFuelKg: 110, TyreCompoundType.Soft, new TyreAges(0));
        
List<Pitstop> pitstops = [new Pitstop(18, TimeSpan.FromSeconds(24), TyreCompoundType.Hard, new TyreAges(0))];
        
RaceStrategy raceStrategy = new RaceStrategy(startStrategy, pitstops);

TyreStatistics[] tyreStrengthConfigs =
[
    new(TyreCompoundType.Soft, TimeSpan.Zero, TimeSpan.FromMilliseconds(200)),
    new(TyreCompoundType.Medium, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(100)),
    new(TyreCompoundType.Hard, TimeSpan.FromMilliseconds(1000), TimeSpan.FromMilliseconds(65)),
];
        
TimeSpan timeLossPerFuelKg = TimeSpan.FromMilliseconds(30);
TimeSpan baseLapTime = TimeSpan.FromSeconds(97.5);
        
float fuelLossPerLapPerKg = 1.5f;

FuelStatistics fuelStatistics = new(fuelLossPerLapPerKg, timeLossPerFuelKg);
CircuitStatistics circuitStatistics = new(totalLaps: 72, baseLapTime, ref fuelStatistics,  tyreStrengthConfigs);

RaceConfiguration raceConfiguration = new(raceStrategy, circuitStatistics);

LinearFuelAndTyreDegRaceSimulator linearRaceSimulator = new();
var result = linearRaceSimulator.Run(raceConfiguration);

Console.WriteLine(result);