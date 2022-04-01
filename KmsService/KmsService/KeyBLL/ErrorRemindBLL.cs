/*
 * 创建人：武梓龙
 * 创建日期：2022年3月12日19:45:39
 * 描述：发生错误时获取的信息
 */
using KmsService.DAL;
using KmsService.Entity;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;

namespace KmsService.KeyBLL
{
    /// <summary>
    /// 错误信息类
    /// </summary>
    public class ErrorRemindBLL
    {
        /// <summary>
        /// 发生错误时获取的信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="eventID">日程ID</param>
        /// <param name="RoomName">会议室名称</param>
        /// <returns>错误信息实体</returns>
        public ErrorInfoEntity ErrorRemind(string userID,string eventID,string RoomName)
        {
            SelectCalendar selectCalendar = new SelectCalendar();
            SelectCalendarModel calendarModel = new SelectCalendarModel();
            calendarModel= selectCalendar.SelectCalendarInfo(userID, "primary", eventID);
            ErrorInfoEntity errorInfo = new ErrorInfoEntity();
            errorInfo.Name = calendarModel.organizer.displayName;
            errorInfo.Time = calendarModel.start.dateTime + "-" + calendarModel.end.dateTime;
            errorInfo.RoomName = RoomName;
            SelectRoomInfoDAL selectRoomInfo = new SelectRoomInfoDAL();
            RoomInfoEntity roomInfo= selectRoomInfo.SelectRoomInfo(RoomName);
            errorInfo.LockNumber = roomInfo.LockNumber;
            return errorInfo;
        }
    }
}