/*
 * 创建人：武梓龙
 * 创建时间：2022年2月12日10点27分
 * 描述：线上会议和周期性日程不推送会议室类
 */

using KmsService.DingDingInterface;

namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    /// 特殊限制类
    /// </summary>
    public class SpecialLimitBLL : CalendarHandlerBLL
    {
        /// <summary>
        /// 线上会议和周期性日程不推送会议室
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉钉ID</param>
        /// <returns>会议室名称</returns>
        public override string CalendarPushRoomBLL(string calendarID, string userID)
        {
            string value = null;
            if (calendarID.Contains("_"))
            {
                return value;
            }
            
            SelectCalendar selectCalendar = new SelectCalendar();
             calendarModel = selectCalendar.SelectCalendarInfo(userID, "primary", calendarID);
            //判断周期性日程和线上会议都不为null
            if (calendarModel.recurrence != null || calendarModel.onlineMeetingInfo != null)
            {
                return value;
            }
            else
            {
                return successor.CalendarPushRoomBLL(calendarID, userID);
            }
            
        }


    }
}