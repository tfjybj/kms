using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 月推送任务定时
    /// </summary>
    public class MonthPushReportJob
    {
        /// <summary>
        /// 创建每月推送定时任务
        /// </summary>
        public void MonthPushTask()
        {
            //创建调度单元
            Task<IScheduler> tsk = StdSchedulerFactory.GetDefaultScheduler();
            IScheduler scheduler = tsk.Result;
            //2.创建一个具体的作业即job (具体的job需要单独在一个文件中执行)
            IJobDetail job = JobBuilder.Create<MonthPushReport>().WithIdentity("0 0 10 1 * ?").Build();
            //3.创建并配置一个触发器即trigger   1s执行一次
            ITrigger _CronTrigger = TriggerBuilder.Create()
              .WithIdentity("0 0 10 1 * ?")
              .WithCronSchedule("0 0 10 1 * ?") //秒 分 时 某一天 月 周 年(可选参数)
              .Build();


            //4.将job和trigger加入到作业调度池中
            scheduler.ScheduleJob(job, _CronTrigger);
            //5.开启调度
            scheduler.Start();
        }
    }
}