using BeforeEnd.Log4;
using BeforeEnd.ServiceReference;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace BeforeEnd
{
    public class BeforeEndBLL
    {
        ServiceClient client = new ServiceClient();
        //ILog log = LogManager.GetLogger("BeforeEndBLL");//获取一个日志记录器

        /// <summary>
        /// 发送消息卡片逻辑
        /// </summary>
        public void SendMessageUser()
        {

            //获取所有钉ID和会议结束时间
            List<CalendarInfoEntity> calendarList = client.BeforeMeetingEnd();

            foreach (var item in calendarList)
            {
                string roomName = item.RoomName;     //会议室名称
                string calendarID = item.CalendarID; //日程ID
                string organizerID = item.OrganizerID;    //组织者ID
                DateTime endTime = Convert.ToDateTime(item.EndTime);   //会议结束时间
                DateTime startTime = Convert.ToDateTime(item.StartTime);//会议开始时间
                string sendTime = Convert.ToString(endTime.AddMinutes(-10).ToString("yyyy/MM/dd hh:mm:00")); //发送消息时间

                string nowTime = DateTime.Now.ToLocalTime().ToString("yyyy/MM/dd hh:mm:00");  //获取系统当前时间

                if (Convert.ToDateTime(nowTime) >= Convert.ToDateTime(sendTime))
                {
                    //打印返回数据信息
                    LoggerHelper.Info("查询数据库返回的值：" + "会议室名称:" + roomName + "\n日程ID:" + calendarID + "\n组织者ID:" + organizerID + "\n会议结束时间:" + endTime + "\n发送消息卡片时间" + sendTime);
                    string content = string.Format("请及时归还您{0}申请的{1}钥匙", startTime, roomName);
                    string url = string.Format("http://kms.tfjybj.com/kms/actionapi/SendMessage/SendRotbotText?userID={0}&content={1}",organizerID,content);
                    //调用消息卡片接口
                    //string url = string.Format("http://d-kms.tfjybj.com/kms/actionapi/SendMessage/SendReturnKey?roomName={0}&calendarID={1}&userID={2}", roomName, calendarID, organizerID);
                    string result = HttpHelper.HttpPost(url);

                    LoggerHelper.Info("调用接口返回的结果为：" + result);

                    //更新发送归还钥匙消息卡片状态
                    client.UpdateIsEnd(calendarID);
                }
            }
        }


    }
}
