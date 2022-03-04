using System;
using KmsService.DAL;
using KmsService.Entity;
using KmsService.Log4;

namespace KmsService.KeyBLL
{
    /// <summary>
    /// 管理员领取钥匙类
    /// </summary>
    public class ManagerGetKeyBLL
    {
        /// <summary>
        /// 管理员开锁
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        public void ManagerGetKey(string roomName)
        {
            try
            {
                // 获取锁ID
                SelectRoomInfoDAL selectRoomInfoDAL = new SelectRoomInfoDAL();
                RoomInfoEntity roomInfo = selectRoomInfoDAL.SelectRoomInfo(roomName);
                //智能开锁
                MQTTServer.Instance().OpenLock(roomInfo.LockNumber);

                //更新会议室使用状态
                OpenLockDAL openLock = new OpenLockDAL();
                int result = openLock.UpdateState(roomInfo.LockNumber.ToString());
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("管理员领取钥匙错误信息：" + ex.Message + "堆栈信息：" + ex.StackTrace);
                
            }
        }
    }
}