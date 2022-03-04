namespace KmsService.DingDingModel
{
    public class AgreeRequestModel
    {
        public Request request { get; set; }
    }

    public class Request
    {
        public string actioner_userid { get; set; }
        public string process_instance_id { get; set; }
        public string remark { get; set; }
        public string result { get; set; }
        public long task_id { get; set; }
    }
}