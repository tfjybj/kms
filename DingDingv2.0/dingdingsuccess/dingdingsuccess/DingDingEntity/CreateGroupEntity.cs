using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess
{
    /// <summary>
    /// 创建钉钉群
    /// </summary>
    public class CreateGroupEntity
    {
        public string name { get; set; }
        public string owner { get; set; }
        public string[] useridlist { get; set; }
    }

}