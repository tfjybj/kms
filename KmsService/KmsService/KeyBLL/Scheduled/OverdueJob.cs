/*
 * 创建人：王梦杰
 * 创建时间：2022年2月25日19:52:29
 * 描述：领取钥匙卡片过期的定时任务
 */
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;


namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 卡片过期job
    /// </summary>
    public class OverdueJob
    {
        /// <summary>
        /// 创建一个领取钥匙卡片过期的定时任务
        /// </summary>
        /// <param name="cron">执行时间的cron表达式</param>
        public void OverdueScheduledTask(string cron)
        {
            //创建调度单元
            Task<IScheduler> tsk = StdSchedulerFactory.GetDefaultScheduler();
            IScheduler scheduler = tsk.Result;
            //2.创建一个具体的作业即job (具体的job需要单独在一个文件中执行)
            IJobDetail job = JobBuilder.Create<OverdueScheduled>().WithIdentity(cron).Build();
            //3.创建并配置一个触发器即trigger   1s执行一次
            ITrigger _CronTrigger = TriggerBuilder.Create()
              .WithIdentity(cron)
              .WithCronSchedule(cron) //秒 分 时 某一天 月 周 年(可选参数)
              .Build();
            //4.将job和trigger加入到作业调度池中
            scheduler.ScheduleJob(job, _CronTrigger);
            //5.开启调度
            scheduler.Start();
        }
    }
}