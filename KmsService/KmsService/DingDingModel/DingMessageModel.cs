namespace KmsService.DingDingModel
{
    public class DingMessageModel
    {
        public string chatid { get; set; }
        public string msgtype { get; set; }
        public Link link { get; set; }
    }

    public class Link
    {
        public string messageUrl { get; set; }
        public string picUrl { get; set; }
        public string title { get; set; }
        public string text { get; set; }
    }
}