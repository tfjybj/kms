using System;
using dingdingsuccess.DingDingInterface;
using dingdingsuccess.DingDingEntity;
using dingdingsuccess.KmsServiceReference;
using dingdingsuccess.Log4;
namespace dingdingsuccess.LimitBLL
{

    /// <summary>
    /// 
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
                LoggerHelper.Info("日程组织者：" + calendarDetailsModel.organizer.displayName);
                //查询符合的会议室
                string roomname = client.PushRoom(events, ddID);
                LoggerHelper.Info("判断日程是否需要发送会议室卡片,会议室名称：" + roomname);

                //if (calendarDetailsModel.onlineMeetingInfo != null)
                //{
                //    LoggerHelper.Info("会议类型是否为空："+calendarDetailsModel.onlineMeetingInfo.type);
                //    if (calendarDetailsModel.onlineMeetingInfo.type == "dingtalk")
                //    {
                //        return result;
                //    }
                //}
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


        /// <summary>
        /// 判断日程时间是否合格
        /// </summary>
        /// <param name="state">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        private bool IsTime(DateTime state, DateTime end)
        {
            bool result = false;
            //判断日程开始时间是否合格
            LoggerHelper.Info("判断时间大小：" + DateTime.Compare(state, DateTime.Now));
            if (DateTime.Compare(state, DateTime.Now) < 0)
            {
                return result;
            }

            TimeSpan timeSpan = end - state;
            double time = timeSpan.TotalMinutes;
            BasicDataEntity basicData = client.SelectAllBasicData();
            LoggerHelper.Info("下限时间：" + basicData.LowerTime + "上限时间：" + basicData.UpperTime + "日程分钟数：" + time);
            //判断日程使用时长是否超时
            if (time < basicData.LowerTime)
            {
                
                return false;
            }
            if (time <= basicData.UpperTime)
            {
                return true;
            }
            
            return result;
        }
    }
}