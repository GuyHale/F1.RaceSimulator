namespace SimulatorEngine.Domain.Events;

[Flags]
public enum LapEventType
{
    None = 1 << 0,
    Pitstop = 1 << 1,
    SafetyCar = 1 << 2,
    RedFlag = 1 << 3,
}