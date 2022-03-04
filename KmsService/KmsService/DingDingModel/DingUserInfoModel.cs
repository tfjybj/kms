namespace KmsService.DingDingModel
{
    public class DingUserInfoModel
    {
        public int errcode { get; set; }
        public ReturnValue result { get; set; }
        public string errmsg { get; set; }
    }

    public class ReturnValue
    {
        public string associated_unionid { get; set; }
        public string unionid { get; set; }
        public string device_id { get; set; }
        public int sys_level { get; set; }
        public string name { get; set; }
        public bool sys { get; set; }
        public string userid { get; set; }
    }
}