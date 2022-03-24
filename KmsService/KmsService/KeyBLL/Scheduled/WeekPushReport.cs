using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace KmsService.KeyBLL.Scheduled
{
    /// <summary>
    /// 每周推送报表具体执行
    /// </summary>
    public class WeekPushReport : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}