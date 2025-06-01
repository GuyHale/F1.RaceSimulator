using SimulatorEngine.Domain.Car.Components.Tyres;

namespace SimulatorEngine.Domain.Strategy;

public record RaceStartStrategy(float StartingFuelKg, TyreCompoundType TyreCompound, TyreAges TyreAges);