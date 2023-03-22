using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        private List<ForecastedBlock> _forecast;
        

        public SimultaneityCtrlr(List<ForecastedBlock> forecast, List<FlexibilityResource> allFrs)
        {
            this._forecast = forecast;
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
            foreach (var forecasted in _forecast)
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
        public async Task<bool> ChargeAccepted(FlexibilityResource fr, State state)
        {
            decimal available = this._forecast.Capacity;
            decimal requested = (decimal) 0.0;
            // requested charge exceeds availability
            if (available < requested)
            {
                // access all necessary info: (fr id, soc(b/a), state, Ctrlr time, request accepted (y/n))
                string output =
                    $"{fr.FrId}, {fr.Soc}, {fr.Soc}, {state}, timeslot, {false}";
                await File.AppendAllTextAsync("/home/malte/RiderProjects/EnergyCtrlrAlg", output, Encoding.UTF8);
                fr.FrStatus = FlexibilityResource.Status.idling;
                return false;
            }
            // Request doesn't exceed availability, charge is accepted
            else
            {
                string output =
                    $"{fr.FrId}, {fr.Soc}, {fr.Soc + 5}, {state}, timeslot, {true}";
                await File.AppendAllTextAsync("/home/malte/RiderProjects/EnergyCtrlrAlg", output, Encoding.UTF8);
                // for now: random battery % as charge per period
                // realistically initially faster, slow down as battery is almost full
                fr.Soc += 5;
                if (fr.Soc < 100)
                {
                    fr.FrStatus = FlexibilityResource.Status.charging;
                }
                else
                {
                    fr.FrStatus = FlexibilityResource.Status.charged;
                }
                return true;
            }
        }
    }
}