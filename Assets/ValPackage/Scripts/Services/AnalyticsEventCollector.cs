using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using ValPackage.Common.Extantions;
using ValPackage.Common.SaveLoad;
using Zenject;

namespace ValPackage.Common.Services
{
    /// <summary>
    /// Collect and save all analytics events, register of events. Useful for UnityAnalytics (need create events in dashboard)
    /// </summary>
    public class AnalyticsEventCollector : Analytics
    {
        private const string _saveKey = "analyticsJson";

        private Dictionary<string, string[]> _events = new();
        [Inject] private SaveSystem _saveSystem;



        private void Start()
        {
            if (!_saveSystem.HasKey(SaveFileType.Analytics, _saveKey)) return;

            var json = _saveSystem.LoadString(SaveFileType.Analytics, _saveKey);
            _events = ParseEvents(json);
        }

        public override void SendEvent(AnalyticsData data)
        {
            if (!_events.ContainsKey(data.Name))
            {
                _events.Add(data.Name, data.Parameters.Select(p => p.Key).ToArray());
                _saveSystem.Save(SaveFileType.Analytics, _saveKey, JsonEvents(_events));
                this.Log("saved new event: " + data.Name);
            }

            LogEvent(data);
        }


        const string _eventsSeparator = "|||";
        const string _paramsSeparator = "||";

        private string JsonEvents(Dictionary<string, string[]> events)
        {
            string json = "";

            foreach (var e in events)
            {
                json += e.Key + _paramsSeparator;
                foreach (var par in e.Value)
                    json += par + _paramsSeparator;

                json = json.TrimEnd(_paramsSeparator.ToCharArray());
                json += _eventsSeparator;
            }
            json = json.TrimEnd(_eventsSeparator.ToCharArray());
            return json;
        }

        private Dictionary<string, string[]> ParseEvents(string json)
        {
            Dictionary<string, string[]> events = new();
            var eventLines = json.Split(_eventsSeparator);

            foreach (var line in eventLines)
            {
                string name = line.Substring(0, line.IndexOf(_paramsSeparator));
                var paramiters = line
                    .Remove(0, line.IndexOf(_paramsSeparator) + _paramsSeparator.Length)
                    .Split(_paramsSeparator);
                events.Add(name, paramiters);
            }

            return events;
        }

        [Button]
        private void CopyEventsToBuffer()
        {
            string buffer = "";
            var json = _saveSystem.LoadString(SaveFileType.Analytics, _saveKey);
            var events = ParseEvents(json);

            foreach (var e in events)
            {
                buffer += e.Key + "\t";
                foreach (var param in e.Value)
                    buffer += param + "\t";
                buffer = buffer.TrimEnd() + "\n";
            }
            buffer = buffer.TrimEnd();

            GUIUtility.systemCopyBuffer = buffer;
            Debug.Log("events was copy to buffer");
        }
    }
}