using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyCtrlrAlg.Logger
{
    // todo start this
    public class Logger : ILogger
    {
        private List<LogMessage> _logs = new();
        
        public async Task Log(string requestedCharge, bool chargeAccepted, int frId, int soc)
        {
            LogMessage newLogMessage = new LogMessage(
                DateTime.Now,
                requestedCharge,
                chargeAccepted,
                frId,
                soc);
            this._logs.Add(newLogMessage);
        }
    }

    public interface ILogger
    {
        Task Log(string requestedCharge, bool chargeAccepted, int frId, int soc);
    }
}