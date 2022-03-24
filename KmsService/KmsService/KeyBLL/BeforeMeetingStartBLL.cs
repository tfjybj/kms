using System;
using System.Collections.Generic;
using KmsService.DAL;
using KmsService.Entity;
using KmsService.Log4;
using System.Configuration;
namespace KmsService.KeyBLL
{
    public class BeforeMeetingStartBLL
    {
        /// <summary>
        /// 会议前30分钟发送卡片消息
        /// </summary>
        public void BeforeMeetingStart()
        {
            try
            {
                HttpHelper httphelper = new HttpHelper();
                BeforeMeetingDAL beforeMettingDAL = new BeforeMeetingDAL();

                List<CalendarInfoEntity> TimeAndID = beforeMettingDAL.BeforeMeetingStart();//时间和钉id;

                DateTime time;//计算出的时间

                foreach (var item in TimeAndID)
                {
                    time = Convert.ToDateTime(item.StartTime).AddMinutes(-30);//计算出会议前30分钟的时间
                    string userid = item.OrganizerID;//会议组织人
                    string roomName = item.RoomName; //会议室名称
                    string ScheduleID = item.CalendarID;//日程id
                    if (DateTime.Now >= time)//现在的时间大于计算出来的时间
                    {
                        LoggerHelper.Info("查询数据库中的返回值：" + "会议室名称：" + roomName + "日程id" + ScheduleID + "组织者id" + userid);

                        string url = ConfigurationManager.ConnectionStrings["getKeyCard"].ConnectionString + string.Format("?roomName={0}&calendarID={1}&userID={2}", roomName, ScheduleID, userid);

                        string OutTrackID = httphelper.HttpGet(url);//接收url的返回值 卡片唯一标识返回值

                        LoggerHelper.Info("调用接口返回结果为：" + OutTrackID);//打印返回结果
                        beforeMettingDAL.UpdateOutTrackID(ScheduleID, OutTrackID);

                        beforeMettingDAL.UpdateIsStart(ScheduleID);//给用户发完消息后更改数据库isstart的状态
                    }

                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("发送领取钥匙卡片错误信息："+ex.ToString());
                throw;
            }

        }
    }
}