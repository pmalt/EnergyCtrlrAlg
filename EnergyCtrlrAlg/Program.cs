using System.Collections.Generic;
using System.IO;

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
            var ctrlr = new SimultaneityCtrlr(forecast, allFrs);
            string[] output = new string[] { };
            File.WriteAllLines("/home/malte/RiderProjects/EnergyCtrlrAlg", output);
        }
    }
}