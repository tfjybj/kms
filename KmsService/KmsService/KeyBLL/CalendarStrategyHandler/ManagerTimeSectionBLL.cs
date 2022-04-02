/*
 * 创建人：王梦杰
 * 创建日期：2022年1月11日19:45:39
 * 描述：管理员是否申请此会议室
 */
using KmsService.DAL;
using System;
using System.Collections.Generic;
using KmsService.Entity;
using System.Configuration;
namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    /// 判断管理员是否申请此会议室
    /// </summary>
    public class ManagerTimeSectionBLL : CalendarHandlerBLL
    {
        /// <summary>
        /// 判断管理员是否申请此会议室
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉ID</param>
        /// <returns>会议室名称</returns>
        public override string CalendarPushRoomBLL(string calendarID, string userID)
        {
            string roomName = null;
            TimeSpan userStartSpan = Convert.ToDateTime(calendarModel.start.dateTime).TimeOfDay;
            TimeSpan userEndSpan = Convert.ToDateTime(calendarModel.end.dateTime).TimeOfDay;
            ManagerRecordDAL managerRecord = new ManagerRecordDAL();
            List<ManagerRecordEntity> managerRecords = managerRecord.SelectSameTimeManagerUse(calendarModel.location.displayName);
            foreach (var item in managerRecords)
            {
                TimeSpan managerSpan = Convert.ToDateTime(item.get_time).TimeOfDay;
                if (userStartSpan <= managerSpan && managerSpan <= userEndSpan)
                {
                    string content = string.Format("您申请的{0}会议室已经被管理员：{1}申请了，请您另选其他时间段或申请别的会议室进行会议！",item.key_name,item.manager_name);
                    string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", userID, content);
                    HttpHelper httpHelper = new HttpHelper();
                    httpHelper.HttpPost(url);
                    return roomName;
                }
                else if (successor!=null)
                {
                    return successor.CalendarPushRoomBLL(calendarID, userID);
                }
            }
            return successor.CalendarPushRoomBLL(calendarID, userID);
        }
    }
}