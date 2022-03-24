/*
 * 创建人：王梦杰
 * 创建时间：2022年2月18日14:38:13
 * 描述：同一时间段同一个人不能申请两个会议室
 */
using System;
using System.Collections.Generic;
using KmsService.DAL;
using KmsService.Entity;
using System.Configuration;

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
            if (calendarModel.location.displayName == ConfigurationManager.ConnectionStrings["roomName"].ConnectionString)
            {
                string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", userID, "由于该会议室每天都在进行会议，所以目前暂停对普通用户的使用");
                HttpHelper httpHelper = new HttpHelper();
                httpHelper.HttpPost(url);

            }
            else
            {
                if (calendarInfos.Count > 0)
                {
                    for (int i = 0; i < calendarInfos.Count; i++)
                    {
                        TimeSpan startSpan = Convert.ToDateTime(calendarInfos[i].StartTime).TimeOfDay;
                        TimeSpan endSpan = Convert.ToDateTime(calendarInfos[i].EndTime).TimeOfDay;
                        TimeSpan newStartSpan = calendarModel.start.dateTime.TimeOfDay;
                        TimeSpan newEndSpan = calendarModel.end.dateTime.TimeOfDay;
                        //判断此次申请的时间是否在之前申请的时间之内
                        if ((newStartSpan >= startSpan && newStartSpan <= endSpan) || (newEndSpan >= startSpan && newEndSpan <= endSpan))
                        {
                            string content = string.Format("您{0}-{1}已经申请了{2}会议室,请勿重复申请", Convert.ToDateTime(calendarInfos[i].StartTime).ToString("HH:mm:ss"),  Convert.ToDateTime(calendarInfos[i].EndTime).ToString("HH:mm:ss"), calendarInfos[i].RoomName);
                            string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", userID, content);
                            new HttpHelper().HttpPost(url);
                            return roomName;
                        }
                    }
                }
                //不在申请的时间之内
                return successor.CalendarPushRoomBLL(calendarID, userID);
            }
            return null;

        }
    }
}