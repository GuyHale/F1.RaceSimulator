using SimulatorEngine.Strategy;

namespace SimulatorEngine.Events;

public class PitstopHandler
{
    private readonly List<Action<Pitstop>> _funcs = [];
    
    public static PitstopHandler operator +(PitstopHandler a, Action<Pitstop> b)
    {
        a._funcs.Add(b);
        return a;
    }
    
    public static PitstopHandler operator -(PitstopHandler a, Action<Pitstop> b)
    {
        a._funcs.Remove(b);
        return a;
    }

    public void Invoke(Pitstop context)
    {
        _funcs.ForEach(a => a(context));
    }
}