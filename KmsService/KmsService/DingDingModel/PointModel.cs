namespace KmsService.DingDingModel
{
    public class Point
    {
        public PointModel[] Property { get; set; }
    }

    public class PointModel
    {
        public int integral { get; set; }
        public string pluginId { get; set; }
        public string primaryId { get; set; }
        public string reason { get; set; }
        public string typeKey { get; set; }
        public string userId { get; set; }
    }
}