using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess
{
    /// <summary>
    /// 钉钉创建群返回值
    /// </summary>
    public class CreateGroupReturnEntity
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string chatid { get; set; }
        public string openConversationId { get; set; }
        public int conversationTag { get; set; }
        
    }

}