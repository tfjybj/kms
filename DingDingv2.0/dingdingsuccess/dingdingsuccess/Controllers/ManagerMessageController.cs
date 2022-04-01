/*
 * 创建人：盖鹏军
 * 时间：2022年3月1日10点30分
 * 描述：管理员调用接口
 */
using dingdingsuccess.CardMessageBLL;
using dingdingsuccess.KmsServiceReference;
using dingdingsuccess.Log4;
using System;
using System.Web.Http;
namespace dingdingsuccess.Controllers
{
    /// <summary>
    /// 管理员执行命令类
    /// </summary>
    public class ManagerMessageController : ApiController
    {
        ServiceClient client = new ServiceClient();


        #region 领取全部钥匙
        /// <summary>
        /// 领取全部钥匙
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetAllKey()
        {
            try
            {
                ////client.PushAllRoom();
                //foreach (var item in client.push)
                //{
                //    client.ManagerOpenLock(item.RoomName);
                //    Thread.Sleep(3000);//睡眠3秒，然后继续执行循环
                //}
                return "0";
            }
            catch (Exception e)
            {

                LoggerHelper.Error("管理员领取全部钥匙错误信息：" + e.Message + "   具体信息：" + e.StackTrace);
                return "1111";
            }



        }
        #endregion

        #region 领取单个钥匙
        [HttpGet]
        public string GetSingleKey(string ddID, string data, string name)
        {

            try
            {
                SendCardMessage sendCardMessage = new SendCardMessage();
                string content = data.Substring(11);
                ManagerRecordEntity managerRecord = new ManagerRecordEntity();
                managerRecord.key_name = content;
                //发送管理员领取钥匙卡片并获取卡片id
                managerRecord.get_out_track_id = sendCardMessage.SendManagerRoomCard(ddID, data);
                managerRecord.user_id = ddID;
                managerRecord.manager_name = name;
                managerRecord.is_cancel = "0";
                client.InsertRoomRecord(managerRecord);
                return "0";
            }
            catch (Exception e)
            {

                LoggerHelper.Error("管理员领取单个钥匙错误信息：" + e.Message + "   具体信息：" + e.StackTrace);
                return "1111";
            }


        }
        #endregion



    }
}