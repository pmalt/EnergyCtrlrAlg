using EnergyCtrlrAlg.States;

namespace EnergyCtrlrAlg
{
    public class OutputWriter
    {
        public string Write(int frId, int socBefore, int socAfter, RequestState stateBefore,
            RequestState stateAfter, string timeslot, bool requestAccepted)
        {
            return $"{frId},{socBefore},{socAfter},{stateBefore},{stateAfter},{timeslot},{requestAccepted}";
        }
    }
}