using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using KmsService.DAL;
using KmsService.Entity;
using KmsService.Log4;

namespace KmsService.KeyBLL
{
    public class ManagerGetRoom
    {
        ManagerRecordDAL record = new ManagerRecordDAL();
        /// <summary>
        /// 管理员发送消息获取消息中的关键字
        /// </summary>
        /// <param name="room">管理员发送消息内容</param>
        /// <returns></returns>
        public string GetRoom(string room)
        {
            string roomName = null;
            try
            {
                SelectRoomInfoDAL selectRoomInfo = new SelectRoomInfoDAL();
                if (room.Contains("：")||room.Contains(":"))
                {
                    //中文冒号切割字符串
                    string[] cArray = room.Split('：');
                    foreach (var item in cArray)
                    {
                        roomName = selectRoomInfo.SelectRoomName(item).RoomName;
                       
                    }
                    if (roomName!=null)
                    {
                        return roomName;
                    }
                    else
                    {
                        string[] eArray = room.Split(':');
                        foreach (var item in eArray)
                        {
                            roomName = selectRoomInfo.SelectRoomName(item).RoomName;
                            
                        }
                        return roomName;
                    }


                    //英文冒号切割字符串

                }
                return roomName;
            }
            catch (Exception e)
            {
                LoggerHelper.Error("管理员发送消息获取消息中的关键字错误信息：" + e.Message + "堆栈信息：" + e.StackTrace);
                return roomName;
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
                    throw new Exception("插入失败");
                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("管理员领取钥匙记录插入信息：" + e.Message + "    具体信息：" + e.StackTrace);
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
                    throw new Exception("更新失败");
                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("作废卡片，更新卡片是否可使用记录：" + e.Message + "    具体信息：" + e.StackTrace);
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
                LoggerHelper.Error("管理员领取钥匙记录更新错误信息信息：" + e.Message + "    具体信息：" + e.StackTrace);
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

                LoggerHelper.Error("管理员归还钥匙记录：" + e.Message + "    具体信息：" + e.StackTrace);
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