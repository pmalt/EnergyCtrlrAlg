using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<bool> ChargeAccepted()
        {
            decimal available = this._foreCast.Capacity;
            decimal requested = (decimal) 0.0;
            if (available < requested)
            {
                // todo log everything in a string, create method for that
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}