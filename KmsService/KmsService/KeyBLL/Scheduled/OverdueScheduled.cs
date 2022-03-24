/*
 * 创建人：王梦杰
 * 创建时间：2022年2月28日19:15:17
 * 描述：领取钥匙卡片过期的定时任务
 */
using Quartz;
using System.Threading.Tasks;

namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 卡片过期
    /// </summary>
    public class OverdueScheduled :IJob
    {
        /// <summary>
        /// 领取钥匙卡片过期的定时任务执行逻辑
        /// </summary>
        /// <param name="context">一个上下文包，包含各种环境信息的句柄，提供给Quartz。iObeExecutionContext。执行时的JobDetail实例，并发送到Quartz。执行完成后的ITrigger实例</param>
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => {
                CancelCardBLL cancelCard = new CancelCardBLL();//领取钥匙卡片过期
                cancelCard.Cancel();
            });
        }
    }
}