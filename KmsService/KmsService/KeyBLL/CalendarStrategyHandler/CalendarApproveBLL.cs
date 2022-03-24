/*
 * 创建人：王梦杰
 * 创建时间：2022年1月22日19:59:37
 * 描述：发起日程根据条件限制选择推送会议室的方式
 */
using System;
using KmsService.Log4;

namespace KmsService.KeyBLL.CalendarStrategyHandler
{
    /// <summary>
    /// 发起日程根据条件限制选择推送会议室的方式
    /// </summary>
    public class CalendarApproveBLL
    {
        /// <summary>
        /// 推送会议室
        /// </summary>
        /// <param name="calendarID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string PushRoom(string calendarID, string userID)
        {
            LoggerHelper.Info("用户发起日程调用推送会议室方法：" + calendarID + "、" + userID);
            string roomName = null;
            try
            {
                //设置上下级
                
                CalendarHandlerBLL specialLimit = new SpecialLimitBLL();
                CalendarHandlerBLL sameTimeLimit = new SameTimeLimitBLL();
                CalendarHandlerBLL managerTimeSertion = new ManagerTimeSectionBLL();
                CalendarHandlerBLL noPlaceLimit = new NoPlaceLimitBLL();
                CalendarHandlerBLL placeLimit = new PlaceLimitBLL();
                CalendarHandlerBLL insideWareHouse = new InsideWareHouseBLL();
                CalendarHandlerBLL outsideWareHouse = new OutsideWareHouseBLL();


                specialLimit.SetSuccessor(outsideWareHouse);
                outsideWareHouse.SetSuccessor(insideWareHouse);
                insideWareHouse.SetSuccessor(managerTimeSertion);
                managerTimeSertion.SetSuccessor(sameTimeLimit);
                sameTimeLimit.SetSuccessor(placeLimit);
                placeLimit.SetSuccessor(noPlaceLimit);

                roomName = specialLimit.CalendarPushRoomBLL(calendarID, userID);
                LoggerHelper.Info("用户发起日程推送的会议室名称：" + roomName);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("用户发起日程推送的会议室名称的错误信息：" + ex.Message + Environment.NewLine + "堆栈信息：" + ex.StackTrace + Environment.NewLine + "所需参数：日程ID、userID：" + calendarID + "、" + userID);
            }
            return roomName;
        }
    }
}