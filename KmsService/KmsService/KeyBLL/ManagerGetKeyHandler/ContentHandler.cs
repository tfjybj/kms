﻿/*
 * 创建人：盖鹏军
 * 创建日期：2022年1月11日19:45:39
 * 描述：传入参数是否能够查到会议室职责
 */
using KmsService.DAL;
using KmsService.Log4;
using System;

namespace KmsService.KeyBLL.ManagerGetKeyHandler
{
    /// <summary>
    /// 判断传入参数是否能够查到会议室职责
    /// </summary>
    public class ContentHandler : GetRoomHandler
    {
        /// <summary>
        /// 判断用输入的领取钥匙口令是否正确
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="managerID">管理员钉ID</param>
        /// <returns>会议室名称</returns>
        public override string GetRoom(string roomName, string managerID)
        {
            try
            {
                SelectRoomInfoDAL selectRoomInfo = new SelectRoomInfoDAL();

                
                if (roomName.Contains("："))
                {
                    //中文冒号切割字符串
                    string[] cArray = roomName.Split('：');
                    roomInfo = selectRoomInfo.SelectRoomName(cArray[1]);
                    return successor.GetRoom(roomInfo.RoomName, managerID);
                }
                if (roomName.Contains(":"))
                {
                    string[] cArray = roomName.Split(':');
                    roomInfo = selectRoomInfo.SelectRoomName(cArray[1]);
                    return successor.GetRoom(roomInfo.RoomName, managerID);
                }

                string strText = "您输入的命令格式有误，请按照正确格式输入\n格式如下：\n钥匙：XXX";
                sendMessages.SendBotbotText(managerID, strText);

                return null;
            }
            catch (Exception e)
            {
                LoggerHelper.Error("判断传入参数是否能够查到会议室职责" + e.Message + "\n具体信息：" + e.StackTrace);
                return null;

            }
        }
    }
}