// See https://aka.ms/new-console-template for more information

using SimulatorEngine.Simulators;

Console.WriteLine("Hello, World!");

LinearFuelAndTyreDegRaceSimulator linearRaceSimulator = new();
var result = linearRaceSimulator.Run();

// publish result to kafka topic, display to UI.

Console.WriteLine(result);