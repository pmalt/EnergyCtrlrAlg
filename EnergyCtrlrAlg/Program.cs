using System.Collections.Generic;
using System.IO;
using System.Text;

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
            // todo add outputWriter lines to output[]
            string[] output = new string[] { };
            File.AppendAllLines("/home/malte/RiderProjects/EnergyCtrlrAlg", output, Encoding.UTF8);
        }
    }
}