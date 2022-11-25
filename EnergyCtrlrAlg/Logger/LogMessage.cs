using System;

namespace EnergyCtrlrAlg.Logger
{
    public class LogMessage
    {
        public DateTime Time;
        public string RequestedCharge;
        public bool ChargeAccepted;
        public int FrId;
        public int Soc;
        
        public LogMessage(DateTime time, string requestedCharge, bool chargeAccepted, int frId, int soc)
        {
            this.Time = time;
            this.RequestedCharge = requestedCharge;
            this.ChargeAccepted = chargeAccepted;
            this.FrId = frId;
            this.Soc = soc;
        }
    }
}