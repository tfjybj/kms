using FluentScheduler;

namespace KmsService.KeyBLL
{
    public class Scheduler
    {
        /// <summary>
        /// 启动定时任务
        /// </summary>
        public static void StartUp()
        {
            JobManager.Initialize(new MyRegistry());
        }

        /// <summary>
        /// 停止定时任务
        /// </summary>
        public static void Stop()
        {
            JobManager.Stop();
        }
    }
}