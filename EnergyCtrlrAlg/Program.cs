using System.Collections.Generic;
using EnergyCtrlrAlg.States;

namespace EnergyCtrlrAlg
{
    class Program
    {
        static void Main()
        {
            // startComponents
            // todo foreach period: each FR decides about requesting charge, CP accepts or denies
            var forecast = new List<ForecastedBlock>();
            var allFrs = new List<FlexibilityResource>();        
            var logger = new Logger.Logger();
            var ctrlr = new SimultaneityCtrlr(forecast, allFrs, logger);
            // todo log charges
            // for CP: n. of accepted/denied requests, 
        }
    }
}