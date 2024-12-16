namespace ValPackage.Common.Services
{
    public class UnityAnalytics : Analytics
    {
        public override void SendEvent(AnalyticsData data)
        {
            throw new System.NotImplementedException();
            //AnalyticsService.Instance.CustomData(data.Name, data.Parameters);
            LogEvent(data);
        }
    }
}