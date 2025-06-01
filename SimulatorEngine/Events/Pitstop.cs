using SimulatorEngine.Car.Components;

namespace SimulatorEngine.Events;

public record Pitstop(int Lap,
    TimeSpan Duration,
    TyreCompoundType TyreCompound,
    TyreAges TyreAges);