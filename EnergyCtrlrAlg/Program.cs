using System.Collections.Generic;

namespace EnergyCtrlrAlg
{
    class Program
    {
        static void Main(string[] args)
        {
            // startComponents
            // todo foreach period: each FR decides about requesting charge, CP accepts or denies
            // todo add way to input ForecastedBlock, FlexibilityResource
            var forecast = new List<ForecastedBlock>();
            var allFrs = new List<FlexibilityResource>();        
            var ctrlr = new SimultaneityCtrlr(forecast, allFrs);
        }
    }
}