using System.Collections.Generic;
using ValPackage.Common.Extantions;

namespace ValPackage.Common.Services
{
    public abstract class Analytics : Singleton<Analytics>
    {
        public abstract void SendEvent(AnalyticsData data);

        protected void LogEvent(AnalyticsData data)
        {
            string log = $"event {data.Name} with params:";
            foreach (var paramiter in data.Parameters)
                log += $"\n {paramiter.Key} = {paramiter.Value}";

            this.Log(log);
        }
    }

    public struct AnalyticsData
    {
        public string Name;
        public Dictionary<string, object> Parameters;

        public AnalyticsData(string name, Dictionary<string, object> parameters)
        {
            Name = name;
            Parameters = parameters;
        }
    }
}