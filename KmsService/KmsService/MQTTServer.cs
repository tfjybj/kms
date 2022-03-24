using System;
using System.Configuration;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using KmsService.DAL;
using KmsService.Entity;
using KmsService.KeyBLL;
using KmsService.Log4;
namespace KmsService
{
    public class MQTTServer
    {
        private static MQTTServer mQTTServer;
        private static string CalendarID = null;
        private static string LockState = null;
        public static string ReturnCardID = null;
        private static string XXLockState = null;
        //private MQTTServer()
        //{
        //    //MqttSubscribe(CalendarID);
        //}
        private static string host = "zw.dmsd.tech";
        private MqttClient client = new MqttClient(host);

        private static void Client_MqttMsgUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {

        }

        private static void Client_ConnectionClosed(object sender, EventArgs e)
        {

        }
        public void DisConnect()
        {
            //MqttClient client = new MqttClient(host);
            string[] topic = { "94B97E901E90/StatusData" };
            //await client.DisconnectAsync();
            //client.UnsubscribeAsync(topic);
            client = new MqttClient(host);
            client.ConnectionClosed += Client_ConnectionClosed;

            client.Disconnect();
            client.MqttMsgUnsubscribed += Client_MqttMsgUnsubscribed;
            client.Unsubscribe(topic);
        }




        public static MQTTServer Instance()
        {
            if (mQTTServer == null)
            {
                 mQTTServer = new MQTTServer();
            }
            return mQTTServer;
        }




        /// <summary>
        /// 开锁
        /// </summary>
        /// <param name="content">锁号</param>
        public void OpenLock(string content)
        {
            LoggerHelper.Info("用户点击开锁开启的锁号："+content);
            string topic = "94B97E901E90/ControlData";

            ////实例化Mqtt客户端
            //MqttClient client = new MqttClient(host);

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            // 发布消息主题 "/home/temperature" ，消息质量 QoS 2
            client.Publish(topic, Encoding.UTF8.GetBytes(content), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        }

        /// <summary>
        /// 订阅有参
        /// </summary>
        /// <param name="calendarID">日程ID</param>
        public void MqttSubscribe(string calendarID)
        {
            CalendarID = calendarID;
            string topic = "94B97E901E90/StatusData";

            //// 实例化Mqtt客户端
            //MqttClient client = new MqttClient(host);

            // 注册接收消息事件
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            // 订阅主题 "/home/temperature"， 订阅质量 QoS 2
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }



        /// <summary>
        /// 订阅无参
        /// </summary>
        public void MqttSubscribe()
        {
            LoggerHelper.Info("订阅");
            string topic = "94B97E901E90/StatusData";

            // 实例化Mqtt客户端
            //MqttClient client = new MqttClient(host);

            // 注册接收消息事件
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId);

            // 订阅主题 "/home/temperature"， 订阅质量 QoS 2
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }




        public void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {           // 打印订阅的发布端消息
            string mqttMsg = Encoding.UTF8.GetString(e.Message);

            string xxLockNumber = mqttMsg.Substring(0, 2);
            string xxlockState = mqttMsg.Substring(2, 6);
            LockState = mqttMsg;
            XXLockState = xxlockState;
            if (mqttMsg == xxLockNumber + "isLock")
            {
                LoggerHelper.Info("MQTT订阅消息信息：" + mqttMsg);
                if (CalendarID != null)
                {
                    IsNoReturnKey();
                }
                else
                {
                    ManagerReturnKey(mqttMsg);
                }

            }

        }



        /// <summary>
        /// 动态绑定
        /// </summary>
        public void IsNoReturnKey()
        {
            try
            {
                if (CalendarID != null)
                {

                    //查询日程表
                    SelectCalendarInfoDAL selectCalendar = new SelectCalendarInfoDAL();
                    CalendarInfoEntity calendarInfo = selectCalendar.SelectCalendarInfo(CalendarID);

                    //修改room表中lockstate为xxisLock条件是roomName=calendarInfo.roomID
                    UpdateLockStateDAL updateLock = new UpdateLockStateDAL();
                    updateLock.UpdateLockState(calendarInfo.RoomName, LockState);

                    ////查询t_room_info根据roomName=roomID
                    SelectRoomInfoDAL numberDAL = new SelectRoomInfoDAL();
                    RoomInfoEntity roomInfoEntity = numberDAL.SelectRoomInfo(calendarInfo.RoomName);
                    string xx = roomInfoEntity.LockState.Substring(0, 2);

                    //更新roomInfo表的lockNumber为XX
                    UpdateLockNumberDAL updateLockNumber = new UpdateLockNumberDAL();
                    int uLockStateResult = updateLockNumber.UpdateLockState(calendarInfo.RoomName, xx);

                    //添加归还时间
                    BeforeMeetingDAL beforeMeetingDAL = new BeforeMeetingDAL();
                    int uReturnTimeResult = beforeMeetingDAL.UpdateReturnTime(CalendarID);

                    //未正常归还钥匙扣分
                    ReturnKeyLateBLL returnKeyLateBLL = new ReturnKeyLateBLL();
                    string result = returnKeyLateBLL.ReturnKeyLate(CalendarID, calendarInfo.OrganizerID);
                    LoggerHelper.Info("未正常归还返回值：" + result);
                    CalendarID = null;


                    //钥匙归还成功给予用户一个消息提示
                    if (uLockStateResult > 0 && uReturnTimeResult > 0)
                    {
                        string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", calendarInfo.OrganizerID, calendarInfo.RoomName + "钥匙归还成功");
                    HttpHelper httpHelper = new HttpHelper();
                    httpHelper.HttpPost(url);
                    }
                    else
                    {
                        string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", calendarInfo.OrganizerID, calendarInfo.RoomName + "钥匙归还失败，请联系管理员");
                        HttpHelper httpHelper = new HttpHelper();
                        httpHelper.HttpPost(url);
                    }
                }
            }
            catch (Exception e)
            {
                LoggerHelper.Error("普通用户动态归还钥匙错误信息：" + e.Message + "\n具体信息：" + e.StackTrace);

            }
            finally
            {
                string[] topic = { "94B97E901E90/StatusData" };
                this.client.Unsubscribe(topic);
                
            }


            //string lockNumber = lockState.Substring(0, 2);
            //string lockNumber = lockState.Substring(0, 2);
        }

        /// <summary>
        /// 管理员动态归还钥匙
        /// </summary>
        /// <param name="keyState"></param>
        public void ManagerReturnKey(string keyState)
        {

            string xxLockNumber = keyState.Substring(0, 2);
            string xxlockState = keyState.Substring(2, 6);
            ManagerRecordEntity managerRecordEntity = null;
            try
            {
                ManagerRecordDAL managerRecord = new ManagerRecordDAL();
                 managerRecordEntity = managerRecord.SelectReturnKey(ReturnCardID);
               
                if (ReturnCardID != null)
                {
                    LoggerHelper.Info("管理员动态归还钥匙roomName的值:" + managerRecordEntity.key_name + "\n" + managerRecordEntity.user_id);
                    //修改room表中lockstate为xxisLock条件是roomName=calendarInfo.roomID
                    UpdateLockStateDAL updateLock = new UpdateLockStateDAL();
                    updateLock.UpdateLockState(managerRecordEntity.key_name, LockState);

                    UpdateLockNumberDAL updateLockNumber = new UpdateLockNumberDAL();
                    int uLockState = updateLockNumber.UpdateLockState(managerRecordEntity.key_name, xxLockNumber);

                    ManagerRecordDAL record = new ManagerRecordDAL();
                    int uManagergetkey = record.UpdateReturnKey(ReturnCardID);
                    //更新管理员领取记录，插入归还标识

                    //判断是否信息更新成功
                    if (uLockState > 0 && uManagergetkey > 0)
                    {
                        string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", managerRecordEntity.user_id, managerRecordEntity.key_name + "钥匙归还成功");
                        HttpHelper httpHelper = new HttpHelper();
                        httpHelper.HttpPost(url);
                    }
                    else
                    {
                        string url = ConfigurationManager.ConnectionStrings["textMessage"].ConnectionString + string.Format("?userID={0}&content={1}", managerRecordEntity.user_id, managerRecordEntity.key_name + "钥匙归还失败，请联系开发人员");
                        HttpHelper httpHelper = new HttpHelper();
                        httpHelper.HttpPost(url);
                    }

                    ReturnCardID = null;

                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("管理员动态归还钥匙错误信息：" + e.Message + "\n具体信息：" + e.StackTrace);
            }
            finally
            {
                string[] topic = { "94B97E901E90/StatusData" };
                this.client.Unsubscribe(topic);



            }




        }


    }
}