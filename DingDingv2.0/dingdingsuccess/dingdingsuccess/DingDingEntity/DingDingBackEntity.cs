using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess
{
    /// <summary>
    /// 钉钉回调返回值类
    /// </summary>
    public class DingDingBackEntity
    {
        public string msg_signature { get; set; }
        public string encrypt { get; set; }
        public string timeStamp { get; set; }
        public string nonce { get; set; }
    }
}