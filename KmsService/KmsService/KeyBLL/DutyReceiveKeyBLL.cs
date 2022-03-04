using KmsService.DAL;
using KmsService.Entity;
using System;
using KmsService.Log4;
namespace KmsService.KeyBLL
{
    public class DutyReceiveKeyBLL
    {
        /// <summary>
        /// 推送大厅领取钥匙的卡片消息
        /// </summary>
        /// <param name="userID">钉钉ID</param>
        public void DutyReceiveKey(string userID)
        {
            try
            {
                SelectRoomInfoDAL selectRoomInfo = new SelectRoomInfoDAL();
                RoomInfoEntity roomInfo = selectRoomInfo.SelectRoomInfo("大厅值班");

                HttpHelper httpHelper = new HttpHelper();

                string[] array = roomInfo.LockState.Split('s');
                foreach (string item in array)
                {
                    if (item.ToString() == "Lock")
                    {
                        string url = string.Format("http://d-kms.tfjybj.com/kms/actionapi/SendMessage/SendReceiveCard?roomName=大厅&calendarID=21321&userID={0}", userID);
                        httpHelper.HttpGet(url);
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("推送大厅领取钥匙的卡片消息" + ex.Message + "堆栈信息：" + ex.StackTrace);
                
            }

        }
    }
}