using System;
using System.Collections.Generic;
using KmsService.Entity;
using KmsService.Log4;
using KmsService.DAL;
using System.Configuration;
using KmsService.KeyBLL.Scheduled;
namespace KmsService.KeyBLL
{
    public class BeforeMeetingEndBLL
    {
        public void SendReturnMessage()
        {
            HttpHelper helper = new HttpHelper();
            BeforeMeetingDAL beforeMeetingDAL = new BeforeMeetingDAL();
            //获取所有钉ID和会议结束时间
            List<CalendarInfoEntity> calendarList = beforeMeetingDAL.BeforeMeetingEnd();

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
                    if (item.GetTime != "")
                    {
                        //打印返回数据信息
                        LoggerHelper.Info("查询数据库返回的值：" + "会议室名称:" + roomName + "\n日程ID:" + calendarID + "\n组织者ID:" + organizerID + "\n会议结束时间:" + endTime + "\n发送消息卡片时间" + sendTime);
                        string content = string.Format("请及时归还您{0}申请的{1}钥匙", startTime, roomName);
                        LoggerHelper.Info("会议开始时间：" + startTime);
                        LoggerHelper.Info("会议结束时间：" + endTime);
                        LoggerHelper.Info("发送消息内容：" + content);
                        string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", organizerID, content);
                        //调用消息卡片接口
                        string result = helper.HttpPost(url);

                        LoggerHelper.Info("调用接口返回的结果为：" + result);

                        //更新发送归还钥匙消息卡片状态
                        beforeMeetingDAL.UpdateIsEnd(calendarID);
                    }
                    else
                    {
                        ConferenceEndBLL conferenceEnd = new ConferenceEndBLL();
                        conferenceEnd.GetConferenceEndKey(DateTime.Now.AddMinutes(5).ToString());
                    }

                }
            }
        }
    }
}