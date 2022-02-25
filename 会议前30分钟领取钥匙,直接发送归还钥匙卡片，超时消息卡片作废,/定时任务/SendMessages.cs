using System.Collections.Generic;


namespace 定时任务
{

    /*
     
     功能：给用户发消息的方法，
     */

    public class SendMessages
    {

        public void SendMessageUser()
        {
            PersonReportDAL prd = new PersonReportDAL();
            PushWeeklyReport pwr = new PushWeeklyReport();
            List<string> dd=  prd.AllddID();//获取查到的钉id
            string[] str = new string[] { };
            string result = "";
            for (int i = 0; i < dd.Count; i++)
            {
                result += dd[i] + ",";
            }
            result = result.Substring(0, result.LastIndexOf(','));
            str = result.Split(',');
            Rootobject rootobject = new Rootobject();

                rootobject.dingIds = str;
                rootobject.groupName = "";
                rootobject.messageContent = "请查收您一周的会议使用情况";
                rootobject.messageSingleTitle = "会议室使用情况";
                rootobject.messageSingleUrl = "http://localhost:53727/DataSummary.aspx";
                rootobject.messageTitle = "一周的会议使用情况";
                rootobject.sender = "";
                
                pwr.message(rootobject);//调用message接口，把获取的id传入
            

           
            

            
        }
    }
}