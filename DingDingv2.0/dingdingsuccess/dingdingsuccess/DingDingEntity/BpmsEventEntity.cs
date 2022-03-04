/*
 * 创建人：盖鹏军
 * 时间：2022年2月23日16点54分
 * 描述：审批事件实体
 */

namespace dingdingsuccess.DingDingEntity
{
    /// <summary>
    /// 审批事件实体
    /// </summary>
    public class BpmsEventEntity
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        public string EventType { get; set; }
        /// <summary>
        /// 审批实例id
        /// </summary>
        public string processInstanceId { get; set; }
        /// <summary>
        /// 审批结束时间
        /// </summary>
        public long finishTime { get; set; }
        /// <summary>
        /// 审批实例对应的企业
        /// </summary>
        public string corpId { get; set; }
        /// <summary>
        ///实例标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// finish：审批正常结束（同意或拒绝）、terminate：审批终止（发起人撤销审批单）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 审批实例url，可在钉钉内跳转到审批页面
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 正常结束时result为agree，拒绝时result为refuse，审批终止时没这个值
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 实例创建时间
        /// </summary>
        public long createTime { get; set; }
        /// <summary>
        /// 发起审批实例的员工
        /// </summary>
        public string staffId { get; set; }
        /// <summary>
        /// 审批模板的唯一码
        /// </summary>
        public string processCode { get; set; }
    }


}