/*
 * 创建人：盖鹏军
 * 时间：2022年1月9日08点40分
 * 描述：给用户发送互动卡片消息B层
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using dingdingsuccess.LimitBLL;
using dingdingsuccess.Log4;
namespace dingdingsuccess.CardMessageBLL
{

    /// <summary>
    /// 发送卡片消息B层类
    /// </summary>
    public class SendCardMessage
    {
        #region 发送会议室推送卡片消息
        /// <summary>
        /// 发送会议室推送卡片消息
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="usageTime">使用时间</param>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">用户钉钉ID</param>
        public void SendChoiceRoom(string roomName, string usageTime, string calendarID, string userID)
        {
            try
            {
                string[] sArray = roomName.Split('+');
                LoggerHelper.Info("数组参数：" + sArray[0] + "--" + sArray[1]+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                if (sArray.Length > 1)
                {
                    Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                    {
                    {"title",sArray[0]},
                    {"date",DateTime.Now.ToString()},
                    {"usagetime",usageTime },
                    {"agree","申请" },
                    { "refuse","拒绝"},
                    { "calendarid",calendarID},
                    { "CallbackRouteKey","CardMessage"},//对应会议室申请卡片消息的回调函数的映射地址
                    { "userid",userID},
                    { "specialRoom",sArray[1].ToString()},
                    { "CardTemplateId",GetConfig.GetConfigValue("cardID","PushRoomCard")},
                    { "outTrackId",userID+GetTimeStamp()}
                    };
                    //卡片消息中的参数

                    LoggerHelper.Info("发送会议室推送卡片消息字典参数：" + cardDataCardParamMap.Values+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                    SampleCardMessage.SendCardMessage(cardDataCardParamMap);
                }

            }
            catch (Exception e)
            {
                LoggerHelper.Error("B层发送消息错误日志：" + e.Message + "\n具体信息：" + e.StackTrace);

            }



        }
        #endregion

        #region 发送领取钥匙卡片消息
        /// <summary>
        /// 发送领取钥匙卡片消息
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">用户ID</param>
        public string SendGetKey(string roomName, string calendarID, string userID)
        {
            string result = "1111";
            Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
            {
                { "calendarid",calendarID},
                { "received","领取"},
                { "date",roomName},
                { "userid",userID},
                { "outTrackId",userID+GetTimeStamp()},
                 { "CallbackRouteKey","GetKeyMessage"},//对应领取钥匙卡片消息的回调函数的映射地址
                { "CardTemplateId",GetConfig.GetConfigValue("cardID","GetKeyCard")}
            };

            try
            {
                SampleCardMessage.SendCardMessage(cardDataCardParamMap);
                return result = cardDataCardParamMap["outTrackId"].ToString();
            }
            catch (Exception e)
            {
                LoggerHelper.Error("发送领取钥匙卡片消息错误信息：" + e.Message + "  具体信息：" + e.StackTrace);

            }
            return result;





        }
        #endregion

        #region 发送询问值班卡片

        /// <summary>
        /// 发送询问值班卡片
        /// </summary>
        /// <param name="userID">用户钉钉ID</param>
        /// <returns></returns>
        public string SendInquiryCard(string userID, string content)
        {

            string result = "0";
            try
            {
                Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                {
                    { "userid",userID},
                    { "datetime",content},
                    { "outTrackId",userID+GetTimeStamp()},//标识卡片的唯一ID
                    { "CallbackRouteKey","DutyCallBack"},//对应领取钥匙卡片消息的回调函数的映射地址
                    { "true","1"},
                    { "false","0"},
                    { "CardTemplateId",GetConfig.GetConfigValue("cardID","DutyCard")}
                };
                SampleCardMessage.SendCardMessage(cardDataCardParamMap);
                return result;
            }
            catch (Exception e)
            {
                LoggerHelper.Error("发送询问值班卡片错误信息：" + e.Message + "  具体信息：" + e.StackTrace);
                return result = "1111";

            }




        }


        #endregion

        #region 更新领取钥匙卡片消息
        /// <summary>
        /// 更新领取钥匙卡片消息
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="OutTrackId">外部卡片唯一标识</param>
        public void UpdateGetKey(string roomName, string OutTrackId)
        {

            Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
            {

                { "date",roomName},

                { "outTrackId",OutTrackId},
                 {"CallbackRouteKey","GetKeyMessage"},//对应领取钥匙卡片消息的回调函数的映射地址
                { "CardTemplateId",GetConfig.GetConfigValue("cardID","GetKeyCard")},
                {"overtime", "1" }
            };



            UpdateCardMessage.UdateCard(cardDataCardParamMap);



        }
        #endregion

        #region 发送归还钥匙卡片消息
        /// <summary>
        /// 发送归还钥匙卡片消息
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="calendarID">日程ID</param>
        /// <param name="userID">用户ID</param>
        public string SendReturnKey(string roomName, string calendarID, string userID)
        {
            string returnCardID = null;
            try
            {
                Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                {


                    { "calendarid",calendarID},
                    { "received","归还"},
                    { "date",string.Format("请归还您领取的{0}会议室钥匙。",roomName)},
                    { "PromptText",ConfigurationManager.ConnectionStrings["PromptText"].ConnectionString},
                    { "userid",userID},
                    { "outTrackId",userID+GetTimeStamp()},
                    { "CallbackRouteKey","ReturnKeyMessage"},//对应归还钥匙卡片消息的回调函数的映射地址
                    { "CardTemplateId",GetConfig.GetConfigValue("cardID","ReturnKeyCard")}

                };
                returnCardID = cardDataCardParamMap["outTrackId"];
                SampleCardMessage.SendCardMessage(cardDataCardParamMap);
                return returnCardID;
            }
            catch (Exception e)
            {
                LoggerHelper.Error("发送归还钥匙卡片消息错误信息：" + e.Message + "   具体信息：" + e.StackTrace);
                return returnCardID;
            }


        }
        #endregion

        #region 获取当前时间的时间戳
        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <returns>时间戳</returns>
        private static string GetTimeStamp()
        {
            //DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            //int timeStamp = Convert.ToInt32((DateTime.Now - dateStart).TotalSeconds);

            //TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
            //;
            //long time = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;

            //return ts.TotalSeconds.ToString();
            //获取当前时间戳，截至毫秒
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (DateTime.Now - startTime).TotalMilliseconds;
            return Math.Round(intResult, 0).ToString();
        }
        #endregion

        #region 给管理员发送领取全部钥匙卡片
        /// <summary>
        /// 给管理员发送领取全部钥匙卡片
        /// </summary>
        /// <param name="managerID">管理员ID</param>
        /// <param name="roomName">会议室名称数组形式</param>
        public void SendManagerCard(string managerID, List<string> roomName)
        {
            Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
            {


                { "userid",managerID},
                { "outTrackId",managerID+GetTimeStamp()},
                { "CallbackRouteKey","ManagerGetKey"},//对应归还钥匙卡片消息的回调函数的映射地址
                { "CardTemplateId","3fe7d715-25dc-4b62-ad69-e1384aee0664"}

            };
            //选择卡片放入几个会议室钥匙领取按钮
            switch (roomName.Count)
            {

                case 1:
                    cardDataCardParamMap.Add("btn1", roomName[0]);
                    break;

                case 2:
                    cardDataCardParamMap.Add("btn1", roomName[0]);
                    cardDataCardParamMap.Add("btn2", roomName[1]);
                    break;
                case 3:
                    cardDataCardParamMap.Add("btn1", roomName[0]);
                    cardDataCardParamMap.Add("btn2", roomName[1]);
                    cardDataCardParamMap.Add("btn3", roomName[2]);
                    break;
                case 4:
                    cardDataCardParamMap.Add("btn1", roomName[0]);
                    cardDataCardParamMap.Add("btn2", roomName[1]);
                    cardDataCardParamMap.Add("btn3", roomName[2]);
                    cardDataCardParamMap.Add("btn4", roomName[3]);
                    break;
                case 5:
                    cardDataCardParamMap.Add("btn1", roomName[0]);
                    cardDataCardParamMap.Add("btn2", roomName[1]);
                    cardDataCardParamMap.Add("btn3", roomName[2]);
                    cardDataCardParamMap.Add("btn4", roomName[3]);
                    cardDataCardParamMap.Add("btn5", roomName[4]);
                    break;
                default:
                    cardDataCardParamMap.Add("btn1", roomName[0]);
                    cardDataCardParamMap.Add("btn2", roomName[1]);
                    cardDataCardParamMap.Add("btn3", roomName[2]);
                    cardDataCardParamMap.Add("btn4", roomName[3]);
                    cardDataCardParamMap.Add("btn5", roomName[4]);
                    break;

            }

            SampleCardMessage.SendCardMessage(cardDataCardParamMap);
        }

        #endregion

        #region 给管理员发送领取钥匙卡片
        /// <summary>
        /// 给管理员发送领取全部钥匙卡片
        /// </summary>
        /// <param name="managerID">管理员ID</param>
        /// <param name="roomName">会议室名称数组形式</param>
        public string SendManagerRoomCard(string managerID, string roomName)
        {

            string content = roomName.Substring(11);
            //字典存储卡片所需信息
            Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
            {
                { "userid",managerID},
                { "outTrackId",managerID+GetTimeStamp()},
                { "CallbackRouteKey","ManagerGetKey"},//对应归还钥匙卡片消息的回调函数的映射地址
                { "CardTemplateId",GetConfig.GetConfigValue("cardID","ManagerGetKeyCard")},
                { "agree","领取" },
                { "cancel","取消"},
                { "dataContent",content},
                { "data",roomName}
            };



            SampleCardMessage.SendCardMessage(cardDataCardParamMap);
            return cardDataCardParamMap["outTrackId"].ToString();
        }

        #endregion

        #region 发送管理员归还钥匙卡片消息
        /// <summary>
        /// 发送管理员归还钥匙卡片消息
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <param name="userID">用户ID</param>
        public string SendManagerReturnKey(string roomName, string userID)
        {
            string returnCardID = null;
            try
            {
                Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                {
                    { "received","归还"},
                    { "date",string.Format("请归还您领取的{0}会议室钥匙。",roomName)},
                    { "PromptText",ConfigurationManager.ConnectionStrings["PromptText"].ConnectionString},
                    { "userid",userID},
                    { "outTrackId",userID+GetTimeStamp()},
                    { "CallbackRouteKey","ManagerReturnKey"},//对应归还钥匙卡片消息的回调函数的映射地址
                    { "CardTemplateId",GetConfig.GetConfigValue("cardID","ReturnKeyCard")}

                };
                returnCardID = cardDataCardParamMap["outTrackId"];
                SampleCardMessage.SendCardMessage(cardDataCardParamMap);
                return returnCardID;
            }
            catch (Exception e)
            {
                LoggerHelper.Error("发送管理员归还钥匙卡片消息错误信息：" + e.Message + "   具体信息：" + e.StackTrace);
                return returnCardID;
            }


        }
        #endregion

    }
}