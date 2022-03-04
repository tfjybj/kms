using FluentScheduler;
using System;
using System.Threading;

/*
 时间管理器
 */

namespace KmsService.KeyBLL
{
    public class MyRegistry : Registry
    {
        #region 暂时不用

        //public MyRegistry()
        //{
        //    // 立即执行每两秒一次的计划任务。（指定一个时间间隔运行，根据自己需求，可以是秒、分、时、天、月、年等。）
        //    Schedule<Demo>().ToRunNow().AndEvery(2).Seconds();

        //    // 延迟一个指定时间间隔执行一次计划任务。（当然，这个间隔依然可以是秒、分、时、天、月、年等。）
        //    Schedule<Demo>().ToRunOnceIn(7).Days ();

        //    // 在一个指定时间执行计划任务（最常用。这里是在每天的下午 1:10 分执行）
        //    Schedule(() => Trace.WriteLine("It's 7:00 AM now.")).ToRunEvery(1).Days().At(13, 10);

        //    // 立即执行一个在每月的星期一 3:00 的计划任务（可以看出来这个一个比较复杂点的时间，它意思是它也能做到！）
        //    Schedule<Demo>().ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Friday ).At(7, 0);

        //    // 在同一个计划中执行两个（多个）任务
        //    Schedule<Demo>().AndThen<Demo>().ToRunNow().AndEvery(5).Minutes();
        //}
        #endregion 暂时不用



        public MyRegistry()
        {
            Welcome();
        }

        private void Welcome()
        {
            // 每2秒一次循环
            Schedule<Demo>().ToRunNow().AndEvery(2).Seconds();

            // 5秒后，只一次
            Schedule<Demo>().ToRunOnceIn(5).Seconds();

            //每天晚上21：15分执行
            Schedule(() => Console.WriteLine("Timed Task - Will run every day at 9:15pm: " + DateTime.Now)).ToRunEvery(1).Days().At(21, 15);

            // 每个月的执行
            Schedule(() =>
            {
                Console.WriteLine("Complex Action Task Starts: " + DateTime.Now);
                Thread.Sleep(1000);
                Console.WriteLine("Complex Action Task Ends: " + DateTime.Now);
            }).ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(3, 0);

            //先执行第一个Job、再执行第二个Job;完成后等5秒继续循环
            Schedule<Demo>().AndThen<Demo>().ToRunNow().AndEvery(5).Seconds();
            //直接运行
            Schedule(() => Console.Write("3, "))
                .WithName("[welcome]")
                .AndThen(() => Console.Write("2, "))
                .AndThen(() => Console.Write("1, "))
                .AndThen(() => Console.WriteLine("Live!"));
        }
    }
}