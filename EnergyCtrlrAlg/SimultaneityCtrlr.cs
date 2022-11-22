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

        private Logger.Logger _logger;


        public SimultaneityCtrlr(List<ForecastedBlock> forecast, List<FlexibilityResource> allFrs, Logger.Logger logger)
        {
            this._foreCast = forecast;
            this.AllFrs = allFrs;
            _logger = logger;
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
                // todo log correctly, have pointer to Request
                await _logger.Log("", false, -1, -1);
                return false;
            }
            else
            {
                await _logger.Log("", true, -1, -1);
                return true;
            }
        }
    }
}