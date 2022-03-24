using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 月推送定时任务
    /// </summary>
    public class MonthPushReport : IJob
    {
        /// <summary>
        /// 月推送任务具体执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}