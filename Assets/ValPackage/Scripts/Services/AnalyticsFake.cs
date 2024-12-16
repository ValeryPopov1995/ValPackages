namespace ValPackage.Common.Services
{
    public class AnalyticsFake : Analytics
    {
        public override void SendEvent(AnalyticsData data) { }
    }
}