/*
 * 创建人：盖鹏军
 * 创建日期：2022年1月11日19:45:39
 * 描述：判断管理员是否有正在使用中的记录
 */
using KmsService.DAL;
using KmsService.Entity;


namespace KmsService.KeyBLL.ManagerGetKeyHandler
{
    /// <summary>
    /// 判断管理员是否有正在使用中的记录
    /// </summary>
    public class ManagerOccupiedHandler : GetRoomHandler
    {
        /// <summary>
        /// 判断是否用管理员已经使用此会议室
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="managerID">管理员钉ID</param>
        /// <returns>会议室名称</returns>
        public override string GetRoom(string roomName, string managerID)
        {
            ManagerRecordDAL managerRecord = new ManagerRecordDAL();
            ManagerRecordEntity recordEntity = managerRecord.SelectOccupiedRecord(roomName);
            //判断查询数据库是否用管理员使用此会议室
            if (recordEntity.manager_name != null)
            {

               
                string strText = string.Format("您要使用的会议室现在正在被管理员：{0}使用中。", recordEntity.manager_name);
                sendMessages.SendBotbotText(managerID, strText);
                return null;
            }
            else
            {
                return successor.GetRoom(roomInfo.RoomName, managerID);
            }
        }
    }
}