/*
 * 创建人：王梦杰
 * 创建时间：2022年1月23日14:59:40
 * 描述：日程无地点符合限制类
 */

using System;
using System.Linq;
using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using KmsService.Entity;

namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    /// 无地点符合限制类
    /// </summary>
    public class NoPlaceLimitBLL : CalendarHandlerBLL
    {
        /// <summary>
        /// 推送会议室名称方法
        /// </summary>
        /// <param name="calendarID">日程id</param>
        /// <param name="userID">钉钉id</param>
        /// <returns>会议室名称</returns>
        public override string CalendarPushRoomBLL(string calendarID, string userID)
        {
            string roomName = null;
            //判断日程中有无地点
            if (calendarModel.location == null)
            {
                PushRoomBLL pushRoom = new PushRoomBLL();
                roomName = pushRoom.PushRoom(calendarModel.attendees.Count());
                //判断日程中的地点是否正在被使用
                if (roomName == null)
                {
                    return roomName;
                }
                //取出基本数据中使用下限时间和使用上限时间
                BasicDataBLL basicDataBll = new BasicDataBLL();
                BasicDataEntity basicData = basicDataBll.SelectALLBasicData(roomName);

                int upperTime = basicData.UpperTime;
                int lowerTime = basicData.LowerTime;

                TimeSpan timeSpan = calendarModel.end.dateTime - calendarModel.start.dateTime;
                int useTime = Convert.ToInt32(timeSpan.TotalMinutes);

                //判断是否符合时间限制
                if ((lowerTime <= useTime) || (useTime <= upperTime))
                {
                    //判断是否符合人数限制
                    if (calendarModel.attendees.Count() >= basicData.MinUseNumber)
                    {
                        roomName = roomName + "+true";

                    }
                    else
                    {
                        roomName = roomName + "+false";
                    }
                    return roomName;
                }
                else
                {
                    roomName = basicData.RoomName + "+false";
                }
                return roomName;

            }
            return successor.CalendarPushRoomBLL(calendarID, userID);

        }
    }
}