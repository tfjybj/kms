using System;
using KmsService.DAL;
using KmsService.Entity;
using KmsService.Log4;

namespace KmsService.KeyBLL
{
    public class CalendarDataBLL
    {
        /// <summary>
        /// 获取日程详情
        /// </summary>
        /// <param name="calendarID"></param>
        /// <returns></returns>
        public CalendarInfoEntity calendarDate(string calendarID)
        {
            CalendarInfoEntity calendarInfo = new CalendarInfoEntity();
            try
            {
                SelectCalendarInfoDAL selectAttendPerson = new SelectCalendarInfoDAL();
                calendarInfo = selectAttendPerson.SelectCalendarInfo(calendarID);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用获取日程表详情方法的错误信息" + ex.Message + "堆栈信息：" + ex.StackTrace);
            }
            return calendarInfo;
        }
    }
}