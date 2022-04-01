/*
 * 创建人：武梓龙
 * 时间：2022年3月6日
 * 描述：设置职责链上下级
 */


namespace KmsService.KeyBLL.SendApproveHandler
{
    /// <summary>
    /// 发送审批
    /// </summary>
    public class SendApproveBLL
    {
        /// <summary>
        /// 设置上下级
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">钉ID</param>
        /// <param name="roomName">会议室</param>
        /// <param name="approveType">审批类型</param>
        public void SendApprove(string calendarID, string userID, string roomName, string approveType)
        {
            
            SendApproveHandlerBLL automaticSendApprove = new AutomaticSendApproveBLL();
            SendApproveHandlerBLL ergodicRecord = new ErgodicRecordBLL();
            SendApproveHandlerBLL insertCalendarInfo = new InsertCalendarInfoBLL();

            ergodicRecord.SetSuccessor(insertCalendarInfo);
             insertCalendarInfo.SetSuccessor(automaticSendApprove);

            ergodicRecord.SendApproveBLL(calendarID, userID, roomName, approveType);
        }
    }
}