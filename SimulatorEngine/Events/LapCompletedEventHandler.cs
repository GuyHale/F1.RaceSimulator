namespace SimulatorEngine.Events;

public class LapCompletedEventHandler
{
    private readonly List<Action> _actions = [];
    
    public static LapCompletedEventHandler operator +(LapCompletedEventHandler a, Action b)
    {
        a._actions.Add(b);
        return a;
    }
    
    public static LapCompletedEventHandler operator -(LapCompletedEventHandler a, Action b)
    {
        a._actions.Remove(b);
        return a;
    }

    public void Invoke()
    {
        _actions.ForEach(a => a());
    }
}