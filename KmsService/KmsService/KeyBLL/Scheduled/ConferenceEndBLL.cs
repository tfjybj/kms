/*
 * 创建人：王梦杰
 * 创建时间：2022年2月26日16:02:03
 * 描述：会议结束时发送归还钥匙提醒的代码
 */
using System;
using KmsService.Log4;
namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 会议结束时发送归还钥匙提醒
    /// </summary>
    public class ConferenceEndBLL
    {
        /// <summary>
        /// 开启发送归还钥匙提醒的定时任务
        /// </summary>
        /// <param name="dateTime">定时任务执行时间</param>
        public void GetConferenceEndKey(string dateTime)
        {
            LoggerHelper.Info("发送归还钥匙提醒的定时任务的执行时间："+dateTime);
            //获取cron表达式
            CronGenerade getCron = new CronGenerade();
            string cron = getCron.GetCron(Convert.ToDateTime(dateTime).AddSeconds(5).ToString());
            //创建归还钥匙提醒的定时任务
            EndScheduledJob endScheduledJob = new EndScheduledJob();
            endScheduledJob.EndScheduledTask(cron);
        }
    }
}