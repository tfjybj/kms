using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KmsService.DingDingModel
{
    public class ApproveInstanceModel
    {
        
        
            public string EventType { get; set; }
            public string processInstanceId { get; set; }
            public long finishTime { get; set; }
            public string corpId { get; set; }
            public string title { get; set; }
            public string type { get; set; }
            public string url { get; set; }
            public string result { get; set; }
            public long createTime { get; set; }
            public string staffId { get; set; }
            public string processCode { get; set; }
        

    }
}