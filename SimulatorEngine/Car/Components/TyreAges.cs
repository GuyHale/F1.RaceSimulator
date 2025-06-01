namespace SimulatorEngine.Car.Components;

public record TyreAges(float FrontLeftAge, float FrontRightAge, float RearLeftAge, float RearRightAge)
{
    public TyreAges(float constantAge) : this(constantAge, constantAge, constantAge, constantAge){}
}