using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using start.ServiceReference;
using start.Log4;
using log4net;
namespace start
{
    class Program
    {
        //ILog log = LogManager.GetLogger("Program");//获取一个日志记录器
        ServiceClient service = new ServiceClient();
        static void Main(string[] args)
        {

            //HttpHelper h = new HttpHelper();
            //Console.WriteLine("Hello World!");
            //LoggerHelper.Error("ceshi");
            //LoggerHelper.Monitor("ddd");
            //ScheduleManage.Show();//调用定时器

            ILog log = LogManager.GetLogger("Program");
            LoggerHelper.Info("测试");
                try
            {
                ScheduleManage.Show();
            }
            catch (Exception ex)
            {

                LoggerHelper.Error("调用发送消息卡片接口返回的结果为："+ex.Message);
            }
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

                #region 每天10点给值班人员发送消息卡片
                //每天10点给值班人员发送消息卡片
                IJobDetail dutyJob = JobBuilder.Create<DutyJob>().WithIdentity("完成").Build();
                ITrigger _DutyCronTrigger = TriggerBuilder.Create()
                    .WithIdentity("定时确认")
                    .WithCronSchedule("0 40 9 * * ?")  //每天10点执行定时任务
                    .Build();



                //4.将job和trigger加入到作业调度池中

                scheduler.ScheduleJob(job, _CronTrigger);



                #endregion
            }
        }


        public class ConsoleJob : IJob//执行内容
        {
            public async Task Execute(IJobExecutionContext context)
            {
                await Task.Run(() => {
                    //要处理的逻辑
                    //SendMessages send = new SendMessages();
                    //send.SendMessageUser();

                    BeforeMeetingBLL before = new BeforeMeetingBLL();//会议前30分中领取钥匙
                    before.BeforeMeetingStart();

                    CancelCard cancel = new CancelCard();
                    cancel.Cancel();//消息卡片作废
                    Console.WriteLine("执行一次");    
                });
            }
        }

        #region 每天上午10点发送值班消息卡片
        /// <summary>
        /// 每天上午10点发送值班消息卡片
        /// </summary>
        public class DutyJob : IJob
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
                    ServiceClient client = new ServiceClient();
                    client.PushDutyMsg();
                    //BeforeEndBLL send = new BeforeEndBLL();
                    //send.SendMessageUser();
                    Console.WriteLine("执行一次");
                });
            }
        }
        #endregion 
    }
}
