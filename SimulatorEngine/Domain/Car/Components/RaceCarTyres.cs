using SimulatorEngine.Domain.Car.Components.Tyres;
using SimulatorEngine.Domain.Events;
using SimulatorEngine.Domain.Events.Handling;
using SimulatorEngine.Domain.Strategy;

namespace SimulatorEngine.Domain.Car.Components;

public record RaceCarTyres
{
    public RaceCarTyres(RaceStartStrategy startStrategy)
    {
        TyreCompoundType tyreCompoundType = startStrategy.TyreCompound;
        
        FrontLeft = new Tyre(tyreCompoundType, startStrategy.TyreAges.FrontLeftAge);
        FrontRight = new Tyre(tyreCompoundType, startStrategy.TyreAges.FrontRightAge);
        RearLeft = new Tyre(tyreCompoundType, startStrategy.TyreAges.RearLeftAge);
        RearRight = new Tyre(tyreCompoundType, startStrategy.TyreAges.RearRightAge);
    }
    
    public Tyre FrontLeft { get; }
    public Tyre FrontRight { get; }
    public Tyre RearLeft { get; }
    public Tyre RearRight { get; }

    public void IncrementWear()
    {
        FrontLeft.IncrementWear();
        FrontRight.IncrementWear();
        RearLeft.IncrementWear();
        RearRight.IncrementWear();
    }
    
    public void ChangeTyres(Pitstop pitstop)
    {
        FrontLeft.Change(pitstop.TyreCompound, pitstop.TyreAges.FrontLeftAge);
        FrontRight.Change(pitstop.TyreCompound, pitstop.TyreAges.FrontRightAge);
        RearLeft.Change(pitstop.TyreCompound, pitstop.TyreAges.RearLeftAge);
        RearRight.Change(pitstop.TyreCompound, pitstop.TyreAges.RearRightAge);
    }
}