/*
 * 创建人：王梦杰
 * 创建时间：2022年1月5日09:04:12
 * 描述：给用户发送卡片消息
 */
using System.Collections.Generic;


namespace 定时任务
{

    /*
     功能：给用户发消息的方法，
     */
    public class SendMessages
    {

        /// <summary>
        /// 发送消息
        /// </summary>
        public void SendMessageUser( string[] ddID,string text)
        {
            //PersonReportDAL prd = new PersonReportDAL();
            PushWeeklyReport pwr = new PushWeeklyReport();
           
            
                Rootobject rootobject = new Rootobject();

                rootobject.dingIds = ddID;
                rootobject.groupName = "";
                rootobject.messageContent =text ;
                rootobject.messageSingleTitle = "会议室使用情况!!";
                string url = string.Format("http://192.168.50.207/PersonalDataReport.aspx?{0}", ddID);
                rootobject.messageSingleUrl = url;
                rootobject.messageTitle = "会议使用情况";
                rootobject.sender = "KMS";
                pwr.Message(rootobject);//调用message接口，把获取的id传入
            
        }
    }
}