/*
 * 创建人：武梓龙
 * 时间：2022年3月6日
 * 描述：获取日程信息插入数据库
 */

using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using KmsService.Entity;
using KmsService.Log4;
using System;
using System.Linq;


namespace KmsService.KeyBLL.SendApproveHandler
{
    public class InsertCalendarInfoBLL : SendApproveHandlerBLL
    {
        /// <summary>
        /// 获取日程信息插入数据库
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉ID</param>
        /// <param name="roomName">会议室</param>
        /// <param name="approveType">审批类型</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override string SendApproveBLL(string calendarID, string userID, string roomName, string approveType)
        {
            try
            {
                //调用钉钉接口获取单个日程详情
                SelectCalendar selectCalendar = new SelectCalendar();
                SelectCalendarModel calendarModel = selectCalendar.SelectCalendarInfo(userID, "primary", calendarID);
                //给日程实体赋值
                CalendarInfoEntity calendarInfo = new CalendarInfoEntity();
                calendarInfo.CalendarID = calendarID;
                calendarInfo.Content = calendarModel.summary;
                calendarInfo.StartTime = calendarModel.start.dateTime.ToString();
                calendarInfo.EndTime = calendarModel.end.dateTime.ToString();
                calendarInfo.RoomName = roomName;
                calendarInfo.AttendCount = calendarModel.attendees.Count().ToString();
                calendarInfo.Organizer = calendarModel.organizer.displayName;
                calendarInfo.OrganizerID = userID;
                calendarInfo.CreateTime = Convert.ToString(DateTime.Now);
                calendarInfo.UpdateTime = Convert.ToString(DateTime.Now);
                calendarInfo.IsEnd = 1;
                calendarInfo.IsStart = 1;
                calendarInfo.Approver = approver;
                //插入日程表
                InsertCalendarDateDAL insertCalendar = new InsertCalendarDateDAL();
                insertCalendar.InsertCalendar(calendarInfo);
                LoggerHelper.Info("用户点击申请调用发送审批不自动同意方法：" + calendarID + "、" + userID + "、" + roomName);
                return successor.SendApproveBLL(calendarID, userID, roomName, approveType);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用是否插入数据库的错误信息：" + ex.ToString());
                return null;
            }
           
        }
    }
}