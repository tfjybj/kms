using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KmsService.Log4;
namespace KmsService.KeyBLL.Scheduled
{
    public class ConferenceOverdueBLL
    {
        /// <summary>
        /// 开启发送归还钥匙提醒的定时任务
        /// </summary>
        /// <param name="dateTime">定时任务执行时间</param>
        public void GetConferenceOverdueCard(string dateTime)
        {
            LoggerHelper.Info("卡片过期定时任务执行时间："+dateTime);
            //获取cron表达式
            CronGenerade getCron = new CronGenerade();
            string cron = getCron.GetCron(Convert.ToDateTime(dateTime).AddSeconds(5).ToString());
            //创建归还钥匙提醒的定时任务
            OverdueJob overdueJob = new OverdueJob();
            overdueJob.OverdueScheduledTask(cron);
        }
    }
}