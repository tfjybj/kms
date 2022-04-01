using System;
using dingdingsuccess.DingDingInterface;
using dingdingsuccess.DingDingEntity;
using dingdingsuccess.KmsServiceReference;
using dingdingsuccess.Log4;
namespace dingdingsuccess.LimitBLL
{

    /// <summary>
    /// 处理日程事件信息业务
    /// </summary>
    public class CalendarLimit
    {
        /// <summary>
        /// 后端服务
        /// </summary>
        ServiceClient client = new ServiceClient();
        /// <summary>
        /// 判断日程是否需要发送会议室卡片
        /// </summary>
        /// <param name="unionID">用户的unionid</param>
        /// <param name="events">日程ID</param>
        /// <returns>会议室名称</returns>
        public string IsCalendars(string unionID, string events,string ddID)
        {
            string result = "false";
            DingCalendarDetails dingCalendar = new DingCalendarDetails();
            try
            {
                //查询日程详情
                CalendarDetailsEntity calendarDetailsModel = dingCalendar.SelectCalendarInfo(unionID, events);
                LoggerHelper.Info("日程组织者：" + calendarDetailsModel.organizer.displayName+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                //查询符合的会议室
                string roomname = client.PushRoom(events, ddID);
                LoggerHelper.Info("判断日程是否需要发送会议室卡片,会议室名称：" + roomname+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());

                //判断房间是否空
                if (roomname != null)
                {
                    result = roomname;
                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("日程限制添加判断问题：" + e.Message + "  具体信息：" + e.StackTrace);
            }
            return result;
        }



    }
}