using SimulatorEngine.Car.Components;

namespace SimulatorEngine.Strategy;

public record RaceStartStrategy(float StartingFuelKg, TyreCompoundType TyreCompound, TyreAges TyreAges);