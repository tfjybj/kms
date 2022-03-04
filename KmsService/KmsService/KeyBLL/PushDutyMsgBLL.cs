using System;
using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using KmsService.Entity;
using KmsService.Log4;
namespace KmsService.KeyBLL
{
    public class PushDutyMsgBLL
    {
        /// <summary>
        /// 推送是否为值班人员的卡片消息
        /// </summary>
        /// <param name="calendarID">日程id</param>
        public void PushDutyMsg()
        {
            try
            {
                //获取日程ID
                SelectCalendarIDDAL selectCalendarID = new SelectCalendarIDDAL();
                string calendarID = selectCalendarID.SelectCalendarID();

                //根据日程ID获取信息
                SelectCalendarInfoDAL selectAttendPerson = new SelectCalendarInfoDAL();
                CalendarInfoEntity calendarInfo = selectAttendPerson.SelectCalendarInfo(calendarID);
                //获取单个日程详情
                SelectCalendar selectCalendar = new SelectCalendar();
                SelectCalendarModel calendarModel = selectCalendar.SelectCalendarInfo(calendarInfo.OrganizerID, "primary", calendarID);
                //获取值班人名单
                SelectDutyDAL selectDuty = new SelectDutyDAL();
                DutyInfoEntity dutyInfor = selectDuty.SelectDuty();

                string dt = DateTime.Now.AddDays(1).ToString("MM月dd日");
                DayOfWeek week = Convert.ToDateTime(dt).DayOfWeek;
                string week1 = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(week);
                string dateTime = dt + " " + week1;
                for (int i = 0; i < Convert.ToInt32(calendarInfo.AttendCount); i++)
                {
                    if (calendarModel.attendees[i].displayName == dutyInfor.DutyNameOne || calendarModel.attendees[i].displayName == dutyInfor.DutyNameTwo)
                    {
                        GetDingID getDingID = new GetDingID();
                        string userid = getDingID.unionid(calendarModel.attendees[i].id);
                        string url = string.Format("http://d-kms.tfjybj.com/kms/actionapi/SendMessage/SendInquiryCard?userID={0}&content={1}", userid, dateTime);
                        HttpHelper httpHelper = new HttpHelper();
                        httpHelper.HttpGet(url);
                    }
                }
            
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("推送是否为值班人员的卡片消息"+ex.Message + "堆栈信息：" + ex.StackTrace);
                
            }

}
    }
}