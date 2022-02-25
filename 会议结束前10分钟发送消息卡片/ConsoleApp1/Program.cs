using BeforeEnd;
using BeforeEnd.Log4;
using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog log = LogManager.GetLogger("Program");//获取一个日志记录器

            try
            {
                ScheduleManage.Show();
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用接口返回的结果为：" + ex.Message + Environment.NewLine + "堆栈信息:" + ex.StackTrace);  //打印错误信息         
            }
        }
    }

    /// <summary>
    /// 定时任务
    /// </summary>
    public class ScheduleManage
    {

        /// <summary>
        /// 定时任务
        /// </summary>
        public static void Show()
        {

            //创建调度单元
            Task<IScheduler> tsk = StdSchedulerFactory.GetDefaultScheduler();
            IScheduler scheduler = tsk.Result;
            //2.创建一个具体的作业即job (具体的job需要单独在一个文件中执行)
            IJobDetail job = JobBuilder.Create<ConsoleJob>().WithIdentity("完成").Build();
            //3.创建并配置一个触发器即trigger   1s执行一次
            ITrigger _CronTrigger = TriggerBuilder.Create()
              .WithIdentity("定时确认")
              .WithCronSchedule("0 */1 * * * ?") //秒 分 时 某一天 月 周 年(可选参数)每周日下午五点执行一次
                                                 // .WithCronSchedule("*/5 * * * * ?") //秒 分 时 某一天 月 周 年(可选参数)每5秒执行一次
              .Build();

            //4.将job和trigger加入到作业调度池中
            scheduler.ScheduleJob(job, _CronTrigger);
            //5.开启调度
            scheduler.Start();
            Console.ReadLine();
        }


        /// <summary>
        /// 任务类，里面放具体执行任务的方法
        /// </summary>
        public class ConsoleJob : IJob
        {
            /// <summary>
            /// 创建要执行的作业
            /// </summary>
            /// <param name="context">执行的上下文</param>
            /// <returns>任务实体</returns>
            public async Task Execute(IJobExecutionContext context)
            {

                await Task.Run(() =>
                {
                    //要处理的逻辑
                    BeforeEndBLL send = new BeforeEndBLL();
                    send.SendMessageUser();

                    Console.WriteLine("执行一次");
                });
            }
        }
    }
}
