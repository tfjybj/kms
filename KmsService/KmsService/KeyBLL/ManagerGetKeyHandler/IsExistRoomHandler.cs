/*
 * 创建人：盖鹏军
 * 创建日期：2022年1月11日19:45:39
 * 描述：判断会议室是否存在
 */

namespace KmsService.KeyBLL.ManagerGetKeyHandler
{
    /// <summary>
    /// 判断会议室是否存在
    /// </summary>
    public class IsExistRoomHandler : GetRoomHandler
    {
        /// <summary>
        /// 判断会议室是否存在
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="managerID">管理员钉ID</param>
        /// <returns>会议室名称</returns>
        public override string GetRoom(string roomName, string managerID)
        {
            if (roomName!=null)
            {
               return successor.GetRoom(roomName, managerID);
            }
            else
            {
                             
                string strText = "并没有您要使用的会议室，请确认您发送的会议室名称是否正确";
                sendMessages.SendBotbotText(managerID,strText);
                return null;
            }
        }
    }
}