using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

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
        /// <param name="roomName"></param>
        /// <param name="managerID"></param>
        /// <returns></returns>
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