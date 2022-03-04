
using Newtonsoft.Json;
using dingdingsuccess.KmsServiceReference;
using dingdingsuccess.Log4;

namespace dingdingsuccess.EventStrategy
{
    /// <summary>
    /// 审批事件
    /// </summary>
    public class BpmsEvent : EventType
    {
        public override void ActEvent(string eventContent)
        {
            try
            {
                ApproveInstanceModel bpmsEvent = JsonConvert.DeserializeObject<ApproveInstanceModel>(eventContent);
                LoggerHelper.Info("审批事件参数："+"审批类型"+bpmsEvent.EventType+"审批id"+bpmsEvent.processInstanceId+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                ServiceClient client = new ServiceClient();
                client.GetApproveResult(bpmsEvent);
            }
            catch (System.Exception e)
            {
                LoggerHelper.Error("审批事件错误信息："+e.Message+"\n具体信息："+e.StackTrace+"\n所属方法："+e.TargetSite);
                
            }


        }
    }
}