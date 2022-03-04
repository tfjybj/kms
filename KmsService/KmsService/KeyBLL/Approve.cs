using KmsService.DingDingInterface;
using KmsService.DingDingModel;

namespace KmsService.KeyBLL
{
    /// <summary>
    /// 审批类，传入参所判断审批是否通过
    /// </summary>
    public class Approve
    {
        /// <summary>
        /// 审批实例ID
        /// </summary>
        /// 保存留待开锁时根据审批ID进行查询审批是否通过。
        public static string processID;

        /// <summary>
        /// 判断审批是否通过
        /// </summary>
        /// <returns>是否通过布尔值</returns>
        public bool Approvalstatus()
        {
            bool flag = false;
            ApproveContent content = new ApproveContent();
            ApproveContentTask contentTask = content.GetApproveContent(processID);
            //判断审批是否同意
            if (contentTask.process_instance.operation_records[0].operation_result == "agree")
            {
                flag = true;
            }
            return flag;
        }

        ///// <summary>
        ///// 开锁
        ///// </summary>
        ///// <returns></returns>
        //public bool Openlock()
        //{
        //    bool flag = false;
        //    ApproveContent content = new ApproveContent();
        //    //查询审批实例获取审批中的审批是否同意、会议室名称
        //    ApproveContentTask contentTask = content.GetApproveContent(processID);
        //    BasicDataDAL basicDataDAL = new BasicDataDAL();
        //    //接受查询数据库返回的会议室钥匙锁号
        //    string code= basicDataDAL.SelectLock(contentTask.process_instance.form_component_values[0].value);
        //    //判断审批是否同意
        //    if (contentTask.process_instance.operation_records[0].operation_result == "agree")
        //    {
        //        flag = true;
        //        MQTTServer mQTT = MQTTServer.Instance();
        //        mQTT.OpenLock(code);
        //    }
        //    return flag;
        //}

        /// <summary>
        /// 创建审批
        /// </summary>
        /// <param name="sendApproveModel">审批参数，具体会议室钥匙、时间、人员</param>
        public void CreateApprove(SendApproveModel sendApproveModel)
        {
            SendApproveExample sendApprove = new SendApproveExample();
            //创建审批

            processID = sendApprove.SendApprove(sendApproveModel).process_instance_id;
        }
    }
}