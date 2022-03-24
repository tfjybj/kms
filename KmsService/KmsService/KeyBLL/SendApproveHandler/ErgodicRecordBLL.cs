/*
 * 创建人：武梓龙
 * 时间：2022年3月6日
 * 描述：判断是否存在重复日程
 */

using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using KmsService.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace KmsService.KeyBLL.SendApproveHandler
{
    public class ErgodicRecordBLL : SendApproveHandlerBLL
    {
        /// <summary>
        /// 判断是否存在重复日程
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉ID</param>
        /// <param name="roomName">会议室名称</param>
        /// <param name="approveType">审批类型</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public override string SendApproveBLL(string calendarID, string userID, string roomName, string approveType)
        {
            //判断日程表中是否存在此日程记录
            CalendarInfoEntity calendarInfoEntity = selectCalendarInfo.SelectCalendarInfo(calendarID);
            if (calendarInfoEntity.CalendarID == null)
            {
                //调用钉钉接口获取单个日程详情
                SelectCalendar selectCalendar = new SelectCalendar();
                SelectCalendarModel calendarModel = selectCalendar.SelectCalendarInfo(userID, "primary", calendarID);
                string startTimeOld = calendarModel.start.dateTime.ToString("yyyy-MM-dd");//获取日程的开始时间
                List<CalendarInfoEntity> calendarinfos = selectCalendarInfo.SelectSameTimePlace(userID, startTimeOld);
                if (calendarinfos.Count > 0)
                {
                    for (int i = 0; i < calendarinfos.Count; i++)
                    {
                        //该用户这一天数据库中的时间
                        TimeSpan startSpan = Convert.ToDateTime(calendarinfos[i].StartTime).TimeOfDay;
                        TimeSpan endspan = Convert.ToDateTime(calendarinfos[i].EndTime).TimeOfDay;
                        //获取日程的开始时间和结束时间
                        TimeSpan newStartSpan = calendarModel.start.dateTime.TimeOfDay;
                        TimeSpan newEndSpan = calendarModel.end.dateTime.TimeOfDay;
                        if (newStartSpan >= startSpan && newStartSpan <= endspan || (newEndSpan >= startSpan && newEndSpan <= endspan))
                        {
                            string content = string.Format("您{0}-{1}申请的{2}会议室已被{3}申请,请勿重复申请", calendarModel.start.dateTime.ToString("HH:mm:ss"), calendarModel.end.dateTime.ToString("HH:mm:ss"), calendarModel.location.displayName, calendarinfos[i].Organizer);
                            string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", userID, content);
                            httpHelper.HttpPost(url);
                            throw new Exception("调用：SendApprove方法，我抛一个异常，它就走不了下面代码了。");
                        }
                    }
                }
                else
                {
                    return successor.SendApproveBLL(calendarID, userID, roomName, approveType);
                }
            }
             
            return null;
        }
    }
}