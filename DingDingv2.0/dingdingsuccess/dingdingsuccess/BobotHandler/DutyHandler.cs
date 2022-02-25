using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess.BobotHandler
{
    public class DutyHandler:Handler
    {


        public override void HandleRequest(string userID, string username, string content)
        {
            if (content.Contains("值班人员："))
            {
                 serviceClient.UpdateDutyName(username, content.Split('：')[1]);
            }
            
        }
    }
}