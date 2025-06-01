using SimulatorEngine.Events;

namespace SimulatorEngine.Car.Components;

public class Engine
{
    public Engine(float initialFuelKg, float fuelLossPerLapKg)
    {
        FuelKg = initialFuelKg;
        FuelLossPerLapKg = fuelLossPerLapKg;
    }
    
    public float FuelKg { get; private set; }
    private float FuelLossPerLapKg { get; }
    
    public void ConsumeFuel()
    {
        FuelKg -= FuelLossPerLapKg;

        if (FuelKg <= 0)
        {
            throw new Exception("Fuel in the engine must be greater than 0");
        }
    }
}