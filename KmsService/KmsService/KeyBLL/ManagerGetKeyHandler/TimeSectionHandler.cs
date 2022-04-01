/*
 * 创建人：盖鹏军
 * 创建日期：2022年1月11日19:45:39
 * 描述：判断时间段是否可以使用钥匙，如果可以则发送领取钥匙卡片并发送提醒，如果不可以则只发送提醒
 */
using KmsService.Log4;
using System;

namespace KmsService.KeyBLL.ManagerGetKeyHandler
{
    /// <summary>
    /// 判断时间段是否可以使用钥匙，如果可以则发送领取钥匙卡片并发送提醒，如果不可以则只发送提醒
    /// </summary>
    public class TimeSectionHandler : GetRoomHandler
    {
        /// <summary>
        /// 判断管理员当下申请会议室时间是否和用户使用会议室的时间冲突
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="managerID">管理员钉ID</param>
        /// <returns>会议室名称</returns>
        public override string GetRoom(string roomName, string managerID)
        {
            HttpHelper httpHelper = new HttpHelper();
            TimeSpan timeSpanNow = new TimeSpan(Convert.ToDateTime(DateTime.Now).Ticks);
            DateTime time = Convert.ToDateTime(calendarInfo.StartTime);
            time = time.AddMinutes(-30);
            LoggerHelper.Info("管理员领取钥匙与用户使用钥匙协调：" + "管理员id" + managerID + "用户id" + calendarInfo.OrganizerID);
            //判断管理员申请时间是否大于用户预约会议室开始时间60分钟
            if ((timeSpanNow.TotalMinutes - dateNow) >= 60)
            {

                string strText = string.Format("当前已有用户已经预约{0}会议室，请您在{1}之前归还钥匙,以免影响该用户正常使用会议室，预约用户：{2}", calendarInfo.RoomName, time.ToString(), calendarInfo.Organizer);
                sendMessages.SendBotbotText(managerID, strText);
                dateNow = int.MaxValue;
                return roomName;
            }
            else
            {

                string
ErrorText = string.Format("您申请的会议室现在已经临近正常预约用户的使用时间了，如果您想要使用此会议室请与预约用户：{0}进行沟通商议", calendarInfo.Organizer);
                sendMessages.SendBotbotText(managerID, ErrorText);
                dateNow = int.MaxValue;
                return null;
            }
        }
    }
}