/*
 * 创建人：王梦杰
 * 创建时间：2022年2月18日08:36:38
 * 描述：推送会议室名称的职责链抽象父类
 */
using KmsService.DingDingModel;
namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    /// 推送会议室名称的职责链抽象父类
    /// </summary>
    public abstract class CalendarHandlerBLL
    {
        protected CalendarHandlerBLL successor;
        protected static SelectCalendarModel calendarModel;
        /// <summary>
        /// 设置上下级
        /// </summary>
        /// <param name="successor">类名</param>
        public void SetSuccessor(CalendarHandlerBLL successor)
        {
            this.successor = successor;
        }

        /// <summary>
        /// 推送会议室方法
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉钉ID</param>
        /// <returns>会议室名称</returns>
        public abstract string CalendarPushRoomBLL(string calendarID, string userID);
    }
}