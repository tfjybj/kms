/*
 * 创建人：盖鹏军
 * 创建时间：2022年1月24日14点14分
 * 描述：管理员领取钥匙职责链
 */

using KmsService.Entity;

namespace KmsService.KeyBLL.ManagerGetKeyHandler
{
    /// <summary>
    /// 管理员领取钥匙职责链父类
    /// </summary>
    public abstract class GetRoomHandler
    {
        protected SendMessages sendMessages = new SendMessages();
        protected GetRoomHandler successor;
        protected static CalendarInfoEntity calendarInfo = new CalendarInfoEntity();//用于记录会议室使用开始时间最小的用户信息
        protected static RoomInfoEntity roomInfo;
        protected static int dateNow = int.MaxValue;//用于判断时间大小排序
        /// <summary>
        /// 推送会议室抽象方法
        /// </summary>
        /// <param name="roomID">会议室名称</param>
        public abstract string GetRoom(string roomName,string managerID);


        /// <summary>
        /// 设置上下级
        /// </summary>
        /// <param name="successor">类名</param>
        public void SetSuccessor(GetRoomHandler successor)
        {
            this.successor = successor;
        }

    }
}