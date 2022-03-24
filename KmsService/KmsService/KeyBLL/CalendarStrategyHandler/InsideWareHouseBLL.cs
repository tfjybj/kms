/*
 * 创建人：王梦杰
 * 创建时间：2022年3月6日10:11:18
 * 描述：库房里间申请职责链
 */
using System.Configuration;
using KmsService.DAL;
using KmsService.Entity;
namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    ///  库房里间申请职责链逻辑类
    /// </summary>
    public class InsideWareHouseBLL : CalendarHandlerBLL
    {
        /// <summary>
        /// 库房里间申请职责链逻辑
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">userID</param>
        /// <returns>会议室名称</returns>
        public override string CalendarPushRoomBLL(string calendarID, string userID)
        {
            string insideRoomName = ConfigurationManager.ConnectionStrings["insideRoom"].ConnectionString;
            if (calendarModel.location.displayName.Contains(insideRoomName))
            {
                SelectCalendarInfoDAL selectCalendarInfo = new SelectCalendarInfoDAL();
                CalendarInfoEntity calendarInfo = selectCalendarInfo.SelectWareHouse(calendarModel.location.displayName);
                if (calendarInfo.CalendarID ==null)
                {
                    return insideRoomName + "+false";
                }
                else
                {
                    string content = string.Format("您申请的库房里间正在被{0}使用，请和{0}沟通好库房里间的使用时间。",calendarInfo.Organizer);
                    string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString+ string.Format("?userID={0}&content={1}", userID, content);
                    HttpHelper httpHelper = new HttpHelper();
                    httpHelper.HttpPost(url);
                    return null;
                }
            }
            else
            {
                return successor.CalendarPushRoomBLL(calendarID, userID);
            }
        }
    }
}