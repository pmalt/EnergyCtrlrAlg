using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnergyCtrlrAlg.States;

namespace EnergyCtrlrAlg
{
    public class SimultaneityCtrlr
    {
        /// <summary>
        /// List of all flexibility resources 
        /// </summary>
        public List<FlexibilityResource> AllFrs;

        private List<ForecastedBlock> _foreCast;
        

        public SimultaneityCtrlr(List<ForecastedBlock> forecast, List<FlexibilityResource> allFrs)
        {
            this._foreCast = forecast;
            this.AllFrs = allFrs;
        }
        
        public void AddFr(FlexibilityResource resource)
        {
            this.AllFrs.Add(resource);
        }

        public void AddMultipleFr(List<FlexibilityResource> flexibilityResources)
        {
            foreach (var resource in flexibilityResources)
            {
                this.AllFrs.Add(resource);
            }
        }

        public ForecastedBlock GetForecastByTime(DateTime time)
        {
            foreach (var forecasted in _foreCast)
            {
                if (forecasted.StartTime == time)
                    return forecasted;
            }

            return null;
        }
        /// <summary>
        /// accept charge request if enough energy available, deny otherwise
        /// </summary>
        /// <returns>true if charge request accepted, false if not</returns>
        public async Task<bool> ChargeAccepted(FlexibilityResource fr)
        {
            decimal available = this._foreCast.Capacity;
            decimal requested = (decimal) 0.0;
            if (available < requested)
            {
                // access all necessary info: (fr id, soc(b/a), state(b/a), Ctrlr time, request accepted (y/n))
                OutputWriter.Write(fr.FrId, fr.Soc, fr.Soc + 1, new DeniedState(this), new NeutralState(this), "", await this.ChargeAccepted(fr));
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}