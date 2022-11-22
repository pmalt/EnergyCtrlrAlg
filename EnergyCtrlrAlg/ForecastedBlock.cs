using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnergyCtrlrAlg
{
    public class ForecastedBlock : IJson<ForecastedBlock>
    {
        public decimal Capacity = new decimal(0.0);
        public DateTime StartTime;
        public DateTime EndTime;
        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this.ToJson());
        }

        public JObject ToJson()
        {
            JObject res = new JObject();
            
            res.Add("capacity", this.Capacity);
            res.Add("StartTime", this.StartTime);
            res.Add("EndTime", this.EndTime);
            
            return res;
        }

        public ForecastedBlock FromJsonString(string jsonString)
        {
            ForecastedBlock res = new ForecastedBlock();
            
            JObject convertedString = JObject.Parse(jsonString);
            res.Capacity = (decimal)convertedString.GetValue("capacity");
            res.StartTime = (DateTime) convertedString.GetValue("StartTime");
            res.EndTime = (DateTime) convertedString.GetValue("EndTime");

            return res;
        }
    }
    

    public interface IJson<T>
    {
        string ToJsonString();
        JObject ToJson();
        T FromJsonString(string jsonString);
    }
}