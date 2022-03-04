namespace KmsService.DingDingModel
{
    /// <summary>
    /// 发起审批后返回值实体
    /// </summary>
    public class SendApproveRe_valueModel
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int errcode { get; set; }

        /// <summary>
        /// 审批ID
        /// </summary>
        public string process_instance_id { get; set; }

        /// <summary>
        /// 返回码描述
        /// </summary>
        public string errmsg { get; set; }

        public string request_id { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public string user_id { get; set; }
    }
}