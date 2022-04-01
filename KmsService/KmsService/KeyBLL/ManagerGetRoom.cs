/*
 * 创建人：盖鹏军
 * 创建日期：2022年3月12日19:45:39
 * 描述：管理员申请会议室
 */
using System;
using System.Threading;
using KmsService.DAL;
using KmsService.Entity;
using KmsService.Log4;

namespace KmsService.KeyBLL
{
    /// <summary>
    /// 管理员申请会议室类
    /// </summary>
    public class ManagerGetRoom
    {
        ManagerRecordDAL record = new ManagerRecordDAL();
        /// <summary>
        /// 管理员发送消息获取消息中的关键字
        /// </summary>
        /// <param name="room">管理员发送消息内容</param>
        /// <returns>会议室名称</returns>
        public string GetRoom(string room,string managerID)
        {
            
            try
            {
                //职责链进行处理管理员使用钥匙的业务
                ManagerGetKeyHandler.GetRoomHandler contentHandler=new ManagerGetKeyHandler.ContentHandler();
                ManagerGetKeyHandler.GetRoomHandler roomHandler =new ManagerGetKeyHandler.RoomHandler();
                ManagerGetKeyHandler.GetRoomHandler timeSectionHandler =new ManagerGetKeyHandler.TimeSectionHandler();
                ManagerGetKeyHandler.GetRoomHandler managerOccupiedHandler =new ManagerGetKeyHandler.ManagerOccupiedHandler();
                ManagerGetKeyHandler.GetRoomHandler userOccupieHandler=new ManagerGetKeyHandler.UserOccupiedHandler();
                ManagerGetKeyHandler.GetRoomHandler isExistRoom=new ManagerGetKeyHandler.IsExistRoomHandler();
                contentHandler.SetSuccessor(isExistRoom);
                isExistRoom.SetSuccessor(userOccupieHandler);
                userOccupieHandler.SetSuccessor(managerOccupiedHandler);
                managerOccupiedHandler.SetSuccessor(roomHandler);
                roomHandler.SetSuccessor(timeSectionHandler);               
                return contentHandler.GetRoom(room, managerID);
            }
            catch (Exception e)
            {
                LoggerHelper.Error("管理员发送消息获取消息中的关键字错误信息：" + e.Message + "\n堆栈信息：" + e.StackTrace);
                return null;
            }


        }


        /// <summary>
        /// 管理员领取钥匙插入记录
        /// </summary>
        /// <param name="managerRecord"></param>
        public void InsertRoomRecord(ManagerRecordEntity managerRecord)
        {
            try
            {
                if (record.InsertRecord(managerRecord) == 0)
                {
                    LoggerHelper.Info("管理员领取钥匙插入记录失败：" + managerRecord.user_id + "\n卡片id：" + managerRecord.get_out_track_id);
                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("管理员领取钥匙记录插入信息：" + e.Message + "\n具体信息：" + e.StackTrace);
            }
        }

        /// <summary>
        /// 管理员取消卡片
        /// </summary>
        /// <param name="cardID"></param>
        public void UpdateCancelRecord(string cardID)
        {
            try
            {
                if (record.UpdateCancelRecord(cardID) == 0)
                {
                    LoggerHelper.Info("更新失败,卡片ID：" + cardID);
                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("作废卡片，更新卡片是否可使用记录：" + e.Message + "\n具体信息：" + e.StackTrace);
            }


        }

        /// <summary>
        /// 领取钥匙更新记录
        /// </summary>
        /// <param name="cardID">领取钥匙卡片ID</param>
        /// <param name="returncardid">归还钥匙卡片ID</param>
        public int UpdateGetKey(string cardID, string returncardid)
        {
            try
            {
                return record.UpdateGetKey(cardID, returncardid);
            }
            catch (Exception e)
            {
                LoggerHelper.Error("管理员领取钥匙记录更新错误信息信息：" + e.Message + "\n具体信息：" + e.StackTrace);
                return 0;
                
            }
        }


        /// <summary>
        /// 管理员归还钥匙
        /// </summary>
        /// <param name="returncardid">归还卡片ID</param>
        /// <returns></returns>
        public void UpdateReturnKey(string returncardid)
        {
          
            try
            {
                MQTTServer.ReturnCardID = returncardid;
                MQTTServer.Instance().MqttSubscribe();
                

                
            }
            catch (Exception e)
            {

                LoggerHelper.Error("管理员归还钥匙记录：" + e.Message + "\n具体信息：" + e.StackTrace);
            }
        }

        /// <summary>
        /// 查询未归还钥匙信息，根据领取钥匙卡片id查询
        /// </summary>
        /// <param name="cardID">领取钥匙卡片</param>
        /// <returns></returns>
        public ManagerRecordEntity SelectGetRecord(string cardID)
        {
            TimeSpan Interval = TimeSpan.FromMilliseconds(100);
            //线程休眠防止快速点击数据库查询信息延时
            Thread.Sleep(Interval);
            return record.SelectGetRecord(cardID);
        }


    }
}