namespace SimulatorEngine.Domain.Events;

public class PitstopEventHandler 
   : IRaceEventHandler<PitstopEvent>
{
   public void Handle(PitstopEvent pitstopEvent)
   {
      pitstopEvent.Lap.PerformPitstop(pitstopEvent.Pitstop);
   }
}