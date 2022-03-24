/*
 * 创建人：王梦杰
 * 创建时间：2022年2月26日16:06:13
 * 描述：会议开始时发送领取钥匙消息的代码
 */
using System;
using KmsService.Log4;

namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 会议开始时发送领取钥匙消息
    /// </summary>
    public class ConferenceStartBLL
    {
        /// <summary>
        /// 开启发送领取钥匙消息的定时任务
        /// </summary>
        /// <param name="dateTime"></param>
        public void GetConferenceStartKey(string dateTime)
        {
            //获取cron表达式
            CronGenerade getCron = new CronGenerade();
            DateTime newDateTime = Convert.ToDateTime(dateTime).AddSeconds(5);
            LoggerHelper.Info("发领取钥匙卡片的执行时间："+newDateTime);
            string cron = getCron.GetCron(Convert.ToDateTime(dateTime).AddSeconds(5).ToString());
            //创建领取钥匙消息的定时任务
            StartScheduledJob startScheduledJob = new StartScheduledJob();
            startScheduledJob.StartScheduledTask(cron);
        }
    }
}