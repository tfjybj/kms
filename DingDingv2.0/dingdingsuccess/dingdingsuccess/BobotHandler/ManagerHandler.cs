using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dingdingsuccess.KmsServiceReference;
namespace dingdingsuccess.BobotHandler
{
    public class ManagerHandler:Handler
    {
        public override void HandleRequest(string userID, string username, string content)
        {
            List<string> result = new List<string>();
            if (content.Contains("钥匙领取"))
            {
                foreach (var item in serviceClient.PushAllRoom())
                {
                    result.Add(item.RoomName);
                }
                cardMessage.SendManagerCard(userID, result);
            }
            else
            {
                successor.HandleRequest(userID, username, content);
            }
                        
        }

        /// <summary>
        /// 获取所有可用会议室名称
        /// </summary>
        /// <returns>会议室集合</returns>
        public List<string> PushAllRoom()
        {
            ServiceClient client = new ServiceClient();
            List<string> result=new List<string>();
            foreach (var item in client.PushAllRoom())
            {
                result.Add(item.RoomName);
            }
            return result;
        }
    }
}