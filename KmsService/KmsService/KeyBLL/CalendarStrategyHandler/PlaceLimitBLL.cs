/*
 * 创建人：武梓龙
 * 创建时间：2022年1月23日14:59:40
 * 描述：日程有地点符合限制类
 */

using System;
using System.Collections.Generic;
using System.Linq;
using KmsService.DAL;
using KmsService.Entity;
using System.Configuration;
namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    /// 有地点符合限制类
    /// </summary>
    public class PlaceLimitBLL : CalendarHandlerBLL
    {
        private HttpHelper httpHelper = new HttpHelper();
        /// <summary>
        /// 推送会议室方法
        /// </summary>
        /// <param name="calendarID">日程id</param>
        /// <param name="userID">钉钉id</param>
        /// <returns>会议室名称</returns>
        public override string CalendarPushRoomBLL(string calendarID, string userID)
        {
            string roomName = null;
            string startTime = calendarModel.start.dateTime.Date.ToString("yyyy-MM-dd");
            string endTime = calendarModel.end.dateTime.Date.ToString("yyyy-MM-dd");

            SelectCalendarInfoDAL selectCalendarInfo = new SelectCalendarInfoDAL();

            //查询同一天内该会议室的使用情况
            List<CalendarInfoEntity> calendarInfos = selectCalendarInfo.SelectCalendarTime(calendarModel.location.displayName, startTime, endTime);
            if (calendarInfos.Count>0)
            {
                for (int i = 0; i < calendarInfos.Count; i++)
                {
                    TimeSpan startSpan = Convert.ToDateTime(calendarInfos[i].StartTime).TimeOfDay;
                    TimeSpan endSpan = Convert.ToDateTime(calendarInfos[i].EndTime).TimeOfDay;
                    TimeSpan newStartSpan = calendarModel.start.dateTime.TimeOfDay;
                    TimeSpan newEndSpan = calendarModel.end.dateTime.TimeOfDay;
                    if ((newStartSpan >= startSpan && newStartSpan <= endSpan) || (newEndSpan >= startSpan && newEndSpan <= endSpan))
                    {
                        if (userID==calendarInfos[i].OrganizerID)
                        {
                           string content = string.Format("您{0}-{1}已经申请了{2}会议室,请勿重复申请", calendarModel.start.dateTime.ToString("HH:mm:ss"), calendarModel.end.dateTime.ToString("HH:mm:ss"), calendarModel.location.displayName);
                            string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", userID, content);
                            httpHelper.HttpPost(url);
                        }
                        else
                        {
                            string content = string.Format("您{0}-{1}申请的{2}会议室已被{3}申请,请勿重复申请", calendarModel.start.dateTime.ToString("HH:mm:ss"), calendarModel.end.dateTime.ToString("HH:mm:ss"), calendarModel.location.displayName, calendarInfos[i].Organizer);
                            string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", userID, content);
                            httpHelper.HttpPost(url);
                            return roomName;
                        }
                    }
                }
            }
            //取出基本数据中使用下限时间和使用上限时间
            BasicDataBLL basicDataBll = new BasicDataBLL();
            BasicDataEntity basicData = basicDataBll.SelectALLBasicData(calendarModel.location.displayName);

            int upperTime = basicData.UpperTime;
            int lowerTime = basicData.LowerTime;

            TimeSpan timeSpan = calendarModel.end.dateTime - calendarModel.start.dateTime;
            int useTime = Convert.ToInt32(timeSpan.TotalMinutes);

            int players = calendarModel.attendees.Count();

            //获取会议室最小使用人数
            SelectRoomInfoDAL selectRoomInfo = new SelectRoomInfoDAL();
            //判断日程有无地点
            if (calendarModel.location != null)
            {
                RoomInfoEntity roomInfoEntity = selectRoomInfo.SelectRoomInfo(calendarModel.location.displayName);
                //判断日程中的地点是否正在被使用
                if (roomInfoEntity.RoomName == null)
                {
                    return roomName;
                }
                //判断是否符合时间限制
                if ((useTime >= lowerTime) || (useTime <= upperTime))
                {
                    //判断人数是否符合限制
                    if (players >= Convert.ToInt32(basicData.MinUseNumber))
                    {
                        roomName = roomInfoEntity.RoomName + "+true";
                    }
                    else
                    {
                        roomName = roomInfoEntity.RoomName + "+false";
                    }
                    return roomName;
                }
                else
                {
                    roomName = roomInfoEntity.RoomName + "+false";
                }
                return roomName;
            }
            return successor.CalendarPushRoomBLL(calendarID, userID);
        }
    }
}