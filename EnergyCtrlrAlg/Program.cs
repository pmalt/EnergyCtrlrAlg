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
            // create string for every action 
            // have string[] to collect output for now
            // call writer (in the end) to write output to txt file (maybe earlier)
            var forecast = new List<ForecastedBlock>();
            var allFrs = new List<FlexibilityResource>();        
            var ctrlr = new SimultaneityCtrlr(forecast, allFrs);
            var writer = new StreamWriterClass();
            // todo define output
            // todo define file path
            string[] output = new string[] { };
            File.WriteAllLines("path", output);
        }
    }
}