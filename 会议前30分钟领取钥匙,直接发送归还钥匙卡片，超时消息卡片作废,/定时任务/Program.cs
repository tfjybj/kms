using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
namespace 定时任务
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ScheduleManage.Show();//调用定时器
        }

        public class ScheduleManage
        {

            public static void Show()//定时器
            {
                //创建调度单元
                Task<IScheduler> tsk = StdSchedulerFactory.GetDefaultScheduler();
                IScheduler scheduler = tsk.Result;
                //2.创建一个具体的作业即job (具体的job需要单独在一个文件中执行)
                IJobDetail job = JobBuilder.Create<ConsoleJob>().WithIdentity("完成").Build();
                //3.创建并配置一个触发器即trigger   1s执行一次
                ITrigger _CronTrigger = TriggerBuilder.Create()
                  .WithIdentity("定时确认")
                  .WithCronSchedule("0 */1 * * * ?") //秒 分 时 某一天 月 周 年(可选参数)
                  .Build()
                  as ITrigger;

                //4.将job和trigger加入到作业调度池中
                scheduler.ScheduleJob(job, _CronTrigger);
                //5.开启调度
                scheduler.Start();
                Console.ReadLine();
            }
        }


        public class ConsoleJob : IJob//执行内容
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Task.Run(() => {
                    //要处理的逻辑
                    SendMessages send = new SendMessages();
                    send.SendMessageUser();
                    Console.WriteLine("执行一次");
                });
            }
        }
    }
}
