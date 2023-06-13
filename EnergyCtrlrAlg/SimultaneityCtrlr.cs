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
            foreach (var forecast in _forecast)
            {
                if (forecast.StartTime == time)
                    return forecast;
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
                // (fr id, soc(b/a), state, Ctrlr time, request sent, request accepted (y/n))
                string output =
                    $"{fr.FrId}, {fr.Soc}, {fr.Soc}, {state}, timeslot, {true}, {false}";
                await File.AppendAllTextAsync("/home/malte/RiderProjects/EnergyCtrlrAlg", output, Encoding.UTF8);
                fr.Status = FlexibilityResource.StatusEnum.Idling;
                return false;
            }
            // Request doesn't exceed availability, charge is accepted
            else
            {
                // (fr id, soc(b/a), state, Ctrlr time, request sent, request accepted (y/n))
                string output =
                    $"{fr.FrId}, {fr.Soc}, {fr.Soc + 5}, {state}, timeslot, {true}, {true}";
                await File.AppendAllTextAsync("/home/malte/RiderProjects/EnergyCtrlrAlg", output, Encoding.UTF8);
                // for now: random battery % as charge per period
                // realistically initially faster, slow down as battery is almost full
                fr.Soc += 5;
                if (fr.Soc < 100)
                {
                    fr.Status = FlexibilityResource.StatusEnum.Charging;
                }
                else
                {
                    fr.Status = FlexibilityResource.StatusEnum.Charged;
                }
                return true;
            }
        }
    }
}