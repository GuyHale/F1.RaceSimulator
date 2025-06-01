namespace SimulatorEngine.Domain.Race;

public class Race
{
    private readonly Dictionary<int, Lap> Laps = [];

    public void AddLap(Lap lap)
    {
        Laps.TryAdd(lap.Number, lap);
    }
}