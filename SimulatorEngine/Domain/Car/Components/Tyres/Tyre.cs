namespace SimulatorEngine.Domain.Car.Components.Tyres;

public class Tyre
{
    public Tyre(TyreCompoundType type, float age)
    {
        Type = type;
        Age = age;
    }
    
    public TyreCompoundType Type { get; private set; }
    public float Age { get; private set; }
    
    public void IncrementWear()
    {
        Age ++;
    }

    public void Change(TyreCompoundType type, float age)
    {
        Age = age;
        Type = type;
    }
}