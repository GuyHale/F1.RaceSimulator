﻿namespace SimulatorEngine.Domain.Car.Components.Tyres;

[Flags]
public enum TyrePositionType
{
    FrontLeft =  1 << 0,
    FrontRight = 1 << 1,
    RearLeft = 1 << 2,
    RearRight = 1 << 3,
}