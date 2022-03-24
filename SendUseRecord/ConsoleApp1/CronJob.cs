/*
 * 创建人：王梦杰
 * 创建2022年1月5日08:48:30
 * 描述：定时任务
 */
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace 定时任务
{
    /// <summary>
    /// 定时任务
    /// </summary>
    class CronJob
    {
        /// <summary>
        /// 程序主入口
        /// </summary>
        /// <param name="args"></param>
       private static void Main()
        {
            ScheduleManage.Show();
            ScheduleManageMonth.Show();
            Console.ReadLine();
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
              .WithCronSchedule("0 0/2 * * * ?  ") //秒 分 时 某一天 月 周 年(可选参数)每周日下午五点执行一次
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
                    //SendMessages send = new SendMessages();
                    //send.SendMessageUser();
                    SendMessages sendreport = new SendMessages();
                    //SendReportMessage sendreport = new SendReportMessage();
                    // PersonReportDAL prd = new PersonReportDAL();
                    PersonReportDAL prd = new PersonReportDAL();
                    List<string> ddWeek = prd.WeekddID();//获取需要一周发送的用户
                    string[] userID = new string[] { };
                    string result = "";
                    string text = "请查收您一周的会议室使用情况！";
                    Console.WriteLine("zx");
                    for (int i = 0; i < ddWeek.Count; i++)
                    {
                        result = ddWeek[i] + ",";
                        result = result.Substring(0, result.LastIndexOf(","));
                        userID = result.Split(',');
                        sendreport.SendMessageUser(userID, text);//调用message接口
                    }
                    Console.WriteLine("执行一次");
                });

            }
        }

    }

    public class ScheduleManageMonth
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
              .WithCronSchedule("0 0/2 * * * ? ") //秒 分 时 某一天 月 周 年(可选参数)每周日下午五点执行一次
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
                    ////要处理的逻辑
                    //SendMessages send = new SendMessages();
                    //send.SendMessageUser();
                    SendMessages sendreport = new SendMessages();
                    //SendReportMessage sendreport = new SendReportMessage();
                    // PersonReportDAL prd = new PersonReportDAL();
                    PersonReportDAL prd = new PersonReportDAL();

                    List<string> ddMonth = prd.MonthddID();//获取需要一个月发送的用户
                    string[] userID = new string[] { };
                    string result = "";
                    string text = "请查收您一个月的会议室使用情况！";
                    Console.WriteLine("zx");
                    for (int i = 0; i < ddMonth.Count; i++)
                    {
                        result = ddMonth[i] + ",";
                        result = result.Substring(0, result.LastIndexOf(","));
                        userID = result.Split(',');
                        sendreport.SendMessageUser(userID, text);//调用message接口
                    }
                    Console.WriteLine("执行一次");
                });

            }
        }

    }
}



