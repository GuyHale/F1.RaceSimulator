using SimulatorEngine.Domain.Car.Components.Tyres;

namespace SimulatorEngine.Domain.Events;

public record Pitstop(int Lap,
    TimeSpan Duration,
    TyreCompoundType TyreCompound,
    TyreAges TyreAges);