/*
 * 创建人：王梦杰
 * 创建时间：2022年2月25日19:52:52
 * 描述：归还钥匙的定时任务执行逻辑
 */
using Quartz;
using System.Threading.Tasks;


namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 归还钥匙的具体Job
    /// </summary>
    public class EndConsoleJob:IJob
    {
        /// <summary>
        /// 归还钥匙的定时任务逻辑方法
        /// </summary>
        /// <param name="context">一个上下文包，包含各种环境信息的句柄，提供给Quartz。iObeExecutionContext。执行时的JobDetail实例，并发送到Quartz。执行完成后的ITrigger实例</param>
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => {
                BeforeMeetingEndBLL before = new BeforeMeetingEndBLL();//会议前30分中领取钥匙
                before.SendReturnMessage();
            });
        }
    }
}