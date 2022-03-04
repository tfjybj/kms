using System.Collections.Generic;

namespace KmsService.DingDingModel
{
    /// <summary>
    /// 发起审批实例实体
    /// </summary>
    public class SendApproveModel
    {
        public List<Form_Component_Values> form_component_values { get; set; }

        /// <summary>
        /// 审批标识码
        /// </summary>
        public int agent_id { get; set; }

        /// <summary>
        /// 审批唯一码
        /// </summary>
        public string process_code { get; set; }

        /// <summary>
        /// 审批人钉钉ID
        /// </summary>
        public string approvers { get; set; }

        /// <summary>
        /// 抄送人
        /// </summary>
        public string cc_list { get; set; }

        /// <summary>
        /// 抄送节点
        /// </summary>
        public string cc_position { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public int dept_id { get; set; }

        /// <summary>
        /// 发起人ID
        /// </summary>
        public string originator_user_id { get; set; }
    }

    public class Form_Component_Values
    {
        /// <summary>
        /// 选项名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 选项内容
        /// </summary>
        public string value { get; set; }
    }
}