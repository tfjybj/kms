namespace KmsService.DingDingModel
{
    public class UserTokenModel
    {
        public string code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public int count { get; set; }
    }

    public class Data
    {
        public string userId { get; set; }
        public string userCode { get; set; }
        public string name { get; set; }
        public string tenantId { get; set; }
        public string token { get; set; }
        public string email { get; set; }
        public int timeStamp { get; set; }
    }
}