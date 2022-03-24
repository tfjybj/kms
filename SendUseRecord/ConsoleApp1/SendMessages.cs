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
           
            //List<string> dd = prd.AllddID();//获取查到的钉id
            //string[] userID = new string[] { };
            //string result = "";

            //给每个用户发送卡片消息
            //for (int i = 0; i < dd.Count; i++)
            //{
            //    result = dd[i] + ",";
            //    result = result.Substring(0, result.LastIndexOf(','));
            //    userID = result.Split(',');
                Rootobject rootobject = new Rootobject();

                rootobject.dingIds = ddID;
                rootobject.groupName = "";
                rootobject.messageContent =text ;
                rootobject.messageSingleTitle = "会议室使用情况!!";
                string url = string.Format("http://192.168.60.140/PersonalDataReport.aspx?{0}", ddID);
                rootobject.messageSingleUrl = url;
                rootobject.messageTitle = "会议使用情况";
                rootobject.sender = "KMS";
                pwr.Message(rootobject);//调用message接口，把获取的id传入
            
        }
    }
}