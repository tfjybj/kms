/*
 * 创建人：王梦杰
 * 创建时间：2022年2月18日14:38:13
 * 描述：同一时间段同一个人不能申请两个会议室
 */
using System;
using System.Collections.Generic;
using KmsService.DAL;
using KmsService.Entity;

namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    /// 同一时间段同一个人不能申请两个会议室类
    /// </summary>
    public class SameTimeLimitBLL : CalendarHandlerBLL
    {
        /// <summary>
        /// 推送会议室方法
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>会议室名称</returns>
        public override string CalendarPushRoomBLL(string calendarID, string userID)
        {
            string roomName = null;

            string startTime = calendarModel.start.dateTime.ToString("yyyy-MM-dd");
            //调用D层查询日程信息
            SelectCalendarInfoDAL selectCalendarInfo = new SelectCalendarInfoDAL();
            List<CalendarInfoEntity> calendarInfos = selectCalendarInfo.SelectSameTimePlace(userID, startTime);
            for (int i = 0; i < calendarInfos.Count; i++)
            {
                TimeSpan startSpan = Convert.ToDateTime(calendarInfos[i].StartTime).TimeOfDay;
                TimeSpan endSpan = Convert.ToDateTime(calendarInfos[i].EndTime).TimeOfDay;

                TimeSpan newStartSpan = calendarModel.start.dateTime.TimeOfDay;
                TimeSpan newEndSpan = calendarModel.end.dateTime.TimeOfDay;
                //判断此次申请的时间是否在之前申请的时间之内
                if ((newStartSpan >= startSpan && newStartSpan <= endSpan) || (newEndSpan >= startSpan && newEndSpan <= endSpan))
                {
                    return roomName;
                }
            }
            //不在申请的时间之内
            return successor.CalendarPushRoomBLL(calendarID, userID);

        }
    }
}