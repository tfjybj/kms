using System;
using System.Linq;
using KmsService.DAL;
using KmsService.Entity;

namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    /// 值班信息类
    /// </summary>
    public class StorageDutyInfoBLL : CalendarHandlerBLL
    {
        /// <summary>
        /// 插入值班日程信息
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉钉ID</param>
        /// <returns>会议室名称</returns>
        public override string CalendarPushRoomBLL(string calendarID, string userID)
        {
            string roomName = null;
            //调用钉钉接口获取单个日程详情

            if (calendarModel.summary.Contains("值班"))
            {
                CalendarInfoEntity calendarInfo = new CalendarInfoEntity();
                calendarInfo.CalendarID = calendarID;
                calendarInfo.Content = calendarModel.summary;
                calendarInfo.StartTime = calendarModel.start.dateTime.ToString();
                calendarInfo.EndTime = calendarModel.end.dateTime.ToString();
                calendarInfo.RoomName = "大厅";
                calendarInfo.AttendCount = calendarModel.attendees.Count().ToString();
                calendarInfo.Organizer = calendarModel.organizer.displayName;
                calendarInfo.OrganizerID = userID;
                calendarInfo.CreateTime = Convert.ToString(DateTime.Now);
                calendarInfo.UpdateTime = Convert.ToString(DateTime.Now);
                calendarInfo.IsEnd = 0;
                calendarInfo.IsStart = 0;

                //插入日程表
                InsertCalendarDateDAL insertCalendar = new InsertCalendarDateDAL();
                insertCalendar.InsertCalendar(calendarInfo);
                return roomName;
            }
            else
            {
                return successor.CalendarPushRoomBLL(calendarID, userID);
            }
        }
    }
}