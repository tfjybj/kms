/*
 * 创建人：盖鹏军
 * 创建日期：2022年1月11日19:45:39
 * 描述：判断会议室房间是否使用职责
 */
using KmsService.DAL;
using System;
using KmsService.Log4;
using KmsService.Entity;
using System.Collections.Generic;

namespace KmsService.KeyBLL.ManagerGetKeyHandler
{
    /// <summary>
    /// 判断会议室房间是否使用职责
    /// </summary>
    public class RoomHandler : GetRoomHandler
    {
        /// <summary>
        /// 判断是否有用户已经预约了此会议室
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="managerID">管理员钉ID</param>
        /// <returns>会议室名称</returns>
        public override string GetRoom(string roomName, string managerID)
        {

            List<CalendarInfoEntity> roomState = new SelectCalendarInfoDAL().SelectUseRecord(roomName);
            if (roomState.Count>0)
            {                
                foreach (var item in roomState)
                {
                    TimeSpan calendarTime = new TimeSpan(Convert.ToDateTime(item.StartTime).Ticks);//用户日程开始时间
                    //获取集合中会议室使用开始时间最小的记录，用于和管理员领取钥匙时间进行比较
                    if (calendarTime.TotalMinutes<dateNow)
                    {
                        dateNow = Convert.ToInt32(calendarTime.TotalMinutes);
                        calendarInfo.OrganizerID = item.OrganizerID;
                        calendarInfo.Organizer=item.Organizer;
                        calendarInfo.StartTime = item.StartTime;
                        calendarInfo.EndTime = item.EndTime;
                    }
                }
                return successor.GetRoom(roomName, managerID);                               
            }
            else
            {
                LoggerHelper.Info("管理员领取钥匙，没有人员预约" + roomName + "会议室"+"管理员ID："+managerID);
                return roomName;

            }
        }


    }
}

