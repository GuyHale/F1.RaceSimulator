using SimulatorEngine.Domain.Race;
using SimulatorEngine.Domain.Simulation;

namespace SimulatorEngine.Application.Simulators;

public interface IRaceSimulation
{
    SimulationResult Run(RaceConfiguration raceConfiguration);
}