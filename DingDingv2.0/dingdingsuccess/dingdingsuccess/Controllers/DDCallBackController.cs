using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Web.Http;
using dingdingsuccess.Log4;
using dingdingsuccess.DingDingEntity;
using System;
using Newtonsoft.Json;
using dingdingsuccess.CardMessageBLL;
using dingdingsuccess.KmsServiceReference;
using System.Text.RegularExpressions;
using dingdingsuccess.BobotHandler;
using dingdingsuccess.DingDingInterface;
namespace dingdingsuccess.Controllers
{
    /// <summary>
    /// 回调接口
    /// </summary>
    public class DDCallBackController : ApiController
    {
        ServiceClient client = new ServiceClient();

        #region 发送推送会议室卡片回调方法
        /// <summary>
        /// 发送推送会议室卡片回调方法
        /// 回调地址对应的映射key值： CardMessage
        /// </summary>
        [HttpPost]
        public void CardMessage()
        {
            string cardID;
            string state;
            string userid;
            //这两句代码是为了接收body体中传入的加密json串
            Request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);
            string content = Request.Content.ReadAsStringAsync().Result;
            //反序列化json串拿去加密字符串
            CallBackMessageEntity model = new CallBackMessageEntity();
            try
            {
                if (content != "")
                {

                    LoggerHelper.Info("推送会议室卡片body体中参数：" + content + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                    JToken json = JToken.Parse(content);
                    cardID = json["outTrackId"].ToString();
                    userid = json["userId"].ToString();
                    LoggerHelper.Info("推送会议室卡片获取唯一卡片消息标识：" + cardID + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                    #region 截取返回值中的信息
                    LoggerHelper.Info("点击事件返回值：" + json["content"].ToString());

                    //只获取content中的cardPrivateData包含的返回值信息
                    string resutle = json["content"].ToString();
                    json = JToken.Parse(resutle);
                    resutle = json["cardPrivateData"].ToString();

                    //只获取 cardPrivateData中包含的params参数
                    json = JToken.Parse(resutle);
                    resutle = json["params"].ToString();
                    json = JToken.Parse(resutle);
                    #endregion


                    //将params中的用户点击按钮以后返回的值放入实体中
                    model.calendarid = json["calendarid"].ToString();
                    model.outTrackId = cardID;
                    model.usagetime = json["usagetime"].ToString();
                    model.date = json["date"].ToString();
                    model.result = json["result"].ToString();
                    model.title = json["title"].ToString();
                    model.userid = userid;

                    LoggerHelper.Info("推送会议室卡片点击返回数据：" + model.date + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                    //创建一个字典，放入卡片中的对应变量参数
                    Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                    {
                        {"title",model.title },
                        {"date",model.date},
                        {"usagetime",model.usagetime },
                        {"outTrackId",cardID},
                        {"userid",model.userid}
                     };

                    if (model.result == "1")
                    {
                        state = "已申请";
                        cardDataCardParamMap.Add("used", state);
                        UpdateCardMessage.UdateCard(cardDataCardParamMap);
                        LoggerHelper.Info("返回参数日程类型：" + json[" specialRoom"].ToString() + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                        LoggerHelper.Info("点击会议室申请卡片同意按钮" + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());


                        client.SendApprove(model.calendarid, model.userid, model.title, json[" specialRoom"].ToString());


                        LoggerHelper.Info("点击返回按钮状态数据：" + model.calendarid + " :" + model.userid + model.title);

                    }
                    else
                    {
                        LoggerHelper.Info("点击会议室申请卡片拒绝按钮" + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                        state = "已拒绝";
                        cardDataCardParamMap.Add("used", state);
                        UpdateCardMessage.UdateCard(cardDataCardParamMap);
                    }

                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("会议室推送卡片消息回调：" + e.Message + "\n具体信息：" + e.StackTrace + "\n参数信息：" + content);
            }



        }

        #endregion

        #region 领取钥匙卡片回调方法
        /// <summary>
        /// 领取钥匙卡片回调方法
        /// 领取钥匙回调地址的对应key值：GetKeyMessage
        /// </summary>
        [HttpPost]
        public void GetKeyMessage()
        {
            //这两句代码是为了接收body体中传入的加密json串
            Request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);
            string content = Request.Content.ReadAsStringAsync().Result;
            CallBackMessageEntity model = new CallBackMessageEntity();
            LoggerHelper.Info("领取钥匙body体中参数：" + content + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
            try
            {
                string cardID;
                if (content != "")
                {

                    JToken json = JToken.Parse(content);
                    cardID = json["outTrackId"].ToString();
                    string userid = json["userId"].ToString();
                    LoggerHelper.Info("获取领取钥匙卡片消息唯一标识：" + cardID + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                    #region 截取返回值中的信息
                    LoggerHelper.Info("点击事件返回值：" + json["content"].ToString());

                    //只获取content中的cardPrivateData包含的返回值信息
                    string resutle = json["content"].ToString();
                    json = JToken.Parse(resutle);
                    resutle = json["cardPrivateData"].ToString();

                    //只获取 cardPrivateData中包含的params参数
                    json = JToken.Parse(resutle);
                    resutle = json["params"].ToString();
                    json = JToken.Parse(resutle);
                    #endregion
                    model.calendarid = json["calendarid"].ToString();
                    model.outTrackId = cardID;
                    model.date = json["date"].ToString();
                    model.result = json["result"].ToString();
                    model.userid = userid;

                    Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                    {
                        {"date",model.date },
                        {"received","已领取"},
                        {"calendarid",model.usagetime },
                        {"outTrackId",cardID}
                    };

                    if (model.result == "1")
                    {
                        //调用后端开锁方法
                        LoggerHelper.Info("钥匙领取传入后端参数：" + userid + "    " + json["calendarid"].ToString() + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                        UpdateCardMessage.UdateCard(cardDataCardParamMap);
                        client.Open(model.userid, model.calendarid);
                    }
                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("领取钥匙回调函数：" + e.Message + "  具体信息：" + e.StackTrace);
            }




        }
        #endregion

        #region 归还钥匙卡片回调方法
        /// <summary>
        /// 归还钥匙卡片回调方法
        /// 归还钥匙回调地址对应Key值： ReturnKeyMessage
        /// </summary>
        [HttpPost]
        public void ReturnKeyMessage()
        {
            //这两句代码是为了接收body体中传入的加密json串
            Request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);
            string content = Request.Content.ReadAsStringAsync().Result;
            CallBackMessageEntity model = new CallBackMessageEntity();
            LoggerHelper.Info("归还钥匙body体中参数：" + content + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
            try
            {
                string cardID;
                if (content != "")
                {

                    JToken json = JToken.Parse(content);
                    cardID = json["outTrackId"].ToString();
                    string userid = json["userId"].ToString();
                    LoggerHelper.Info("获取归还钥匙卡片消息唯一标识：" + cardID + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                    #region 截取返回值中的信息
                    LoggerHelper.Info("归还钥匙卡片点击事件返回值：" + json["content"].ToString() + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                    //只获取content中的cardPrivateData包含的返回值信息
                    string resutle = json["content"].ToString();
                    json = JToken.Parse(resutle);
                    resutle = json["cardPrivateData"].ToString();

                    //只获取 cardPrivateData中包含的params参数
                    json = JToken.Parse(resutle);
                    resutle = json["params"].ToString();
                    json = JToken.Parse(resutle);
                    #endregion
                    model.calendarid = json["calendarid"].ToString();
                    model.outTrackId = cardID;
                    model.date = json["date"].ToString();
                    model.result = json["result"].ToString();


                    Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                    {
                        {"date",model.date },
                        {"received","已归还"},
                        {"calendarid",model.usagetime },
                        {"outTrackId",cardID},
                        { "PromptText",json["PromptText"].ToString()}

                    };
                    if (model.result == "1")
                    {
                        LoggerHelper.Info("钥匙归还点击成功" + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                        UpdateCardMessage.UdateCard(cardDataCardParamMap);
                        client.DynamicReturnKey(model.calendarid);
                    }




                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("归还钥匙回调函数：" + e.Message + "\n具体信息：" + e.StackTrace);
            }
        }

        #endregion

        #region 机器人消息接收回调
        public void ReceiveMessage()
        {
            string content;
            string datatext;
            string userid;
            //这两句代码是为了接收body体中传入的加密json串
            Request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);
            content = Request.Content.ReadAsStringAsync().Result;



            try
            {
                JToken json = JToken.Parse(content);
                datatext = json["text"].ToString();
                userid = json["senderStaffId"].ToString();
                json = JToken.Parse(datatext);
                datatext = json["content"].ToString();

                LoggerHelper.Info("机器人消息接收回调具体的消息内容参数：" + datatext + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                DialogueHandler gradeHandler = new GradeHandler();
                DialogueHandler phoneHandler = new PhoneHandler();
                DialogueHandler resourcesHandler = new ResourcesHandler();
                DialogueHandler basicCongiguration = new BasicConfigurationHandler();

                //phoneHandler.SetSuccessor(gradeHandler);
                //gradeHandler.SetSuccessor(resourcesHandler);
                //resourcesHandler.SetSuccessor(basicCongiguration);

                phoneHandler.SetSuccessor(gradeHandler);
                gradeHandler.SetSuccessor(basicCongiguration);
                basicCongiguration.SetSuccessor(resourcesHandler);

                phoneHandler.HandleRequest(userid, datatext);
            }
            catch (Exception e)
            {

                LoggerHelper.Error("机器人消息接收回调错误信息：" + e.Message + "\n具体信息：" + e.StackTrace);
            }



        }


        #endregion

        #region 管理员钥匙领取回调地址
        /// <summary>
        /// 管理员钥匙领取回调地址
        /// 管理员钥匙领取回调地址的对应key值：ManagerGetKey
        /// </summary>
        [HttpPost]
        public void ManagerGetKey()
        {
            string equestContent;
            string userID;
            string datatext;//返回内容
            string roomName;
            string cardID;
            Request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);
            equestContent = Request.Content.ReadAsStringAsync().Result;
            SendCardMessage sendCard = new SendCardMessage();
            try
            {

                LoggerHelper.Info("管理员钥匙领取回调body参数：" + equestContent + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                JToken json = JToken.Parse(equestContent);
                #region 截取返回值中的信息

                //只获取content中的cardPrivateData包含的返回值信息
                cardID = json["outTrackId"].ToString();
                userID = json["userId"].ToString();

                datatext = json["content"].ToString();

                //只获取 cardPrivateData中包含的params参数
                json = JToken.Parse(datatext);
                datatext = json["cardPrivateData"].ToString();

                json = JToken.Parse(datatext);
                datatext = json["params"].ToString();
                json = JToken.Parse(datatext);
                datatext = json["result"].ToString();

                #endregion
                Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                {
                        {"data",json["data"].ToString()},
                        {"outTrackId",cardID},

                };


                //如果点击同意则领取钥匙并发送归还钥匙卡片更新后端数据，否则取消卡片，并更新后端数据
                if ("1" == datatext)
                {
                    LoggerHelper.Info("管理员钥匙领取·" + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                    ManagerRecordEntity managerRecord = client.SelectGetRecord(cardID);
                    //判断用户是否已经归还，防止多次点击领取出现多个归还钥匙卡片
                    if ("" == managerRecord.get_time)
                    {
                        roomName = json["dataContent"].ToString();
                        client.ManagerOpenLock(roomName);
                        //发送归还钥匙卡片获取卡片ID
                        string returnCardID = sendCard.SendManagerReturnKey(roomName, userID);
                        //后端插入领取钥匙后的信息

                        client.UpdateGetKey(cardID, returnCardID);
                        cardDataCardParamMap.Add("dynamicBtn", "已领取");
                        UpdateCardMessage.UdateCard(cardDataCardParamMap);
                    }


                }
                else
                {
                    cardDataCardParamMap.Add("dynamicBtn", "已取消");
                    UpdateCardMessage.UdateCard(cardDataCardParamMap);
                    client.UpdateCancelRecord(cardID);
                }


            }
            catch (Exception e)
            {
                LoggerHelper.Error("管理员钥匙领取回调地址错误信息：" + e.Message + "   具体信息：" + e.StackTrace);

            }




        }
        #endregion

        #region 管理员归还钥匙回调地址
        /// <summary>
        /// 管理员归还钥匙回调地址
        /// ManagerReturnKey
        /// </summary>
        public void ManagerReturnKey()
        {
            string equestContent;
            string userID;
            string datatext;//返回内容
            string roomName;
            string cardID;
            try
            {
                Request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);
                equestContent = Request.Content.ReadAsStringAsync().Result;
                LoggerHelper.Info("管理员归还钥匙回调地址body参数：" + equestContent + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
                JToken json = JToken.Parse(equestContent);
                #region 截取返回值中的信息

                //只获取content中的cardPrivateData包含的返回值信息
                cardID = json["outTrackId"].ToString();
                userID = json["userId"].ToString();

                datatext = json["content"].ToString();

                //只获取 cardPrivateData中包含的params参数
                json = JToken.Parse(datatext);
                datatext = json["cardPrivateData"].ToString();

                json = JToken.Parse(datatext);
                datatext = json["params"].ToString();
                json = JToken.Parse(datatext);
                datatext = json["result"].ToString();
                roomName = json["date"].ToString();
                #endregion
                Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                {
                     {"date",json["date"].ToString()},
                     {"outTrackId",cardID},
                    { "received","已归还"},
                    { "PromptText",json["PromptText"].ToString()}
                };
                client.UpdateReturnKey(cardID);

                //更新卡片
                UpdateCardMessage.UdateCard(cardDataCardParamMap);
            }
            catch (Exception e)
            {
                LoggerHelper.Error("管理员钥匙归还回调地址错误信息：" + e.Message + "   具体信息：" + e.StackTrace);

            }
        }

        #endregion

        #region 值班询问卡片回调
        /// <summary>
        /// 值班询问卡片回调
        /// DutyCallBack
        /// </summary>
        [HttpPost]
        public void DutyCallBack()
        {
            //这两句代码是为了接收body体中传入的加密json串
            Request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);
            string content = Request.Content.ReadAsStringAsync().Result;
            LoggerHelper.Info("值班询问卡片body体中参数：" + content + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());
            try
            {
                string cardID;
                if (content != "")
                {

                    JToken json = JToken.Parse(content);
                    cardID = json["outTrackId"].ToString();
                    string userid = json["userId"].ToString();
                    LoggerHelper.Info("值班询问卡片消息唯一标识：" + cardID + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                    #region 截取返回值中的信息
                    LoggerHelper.Info("值班询问卡片点击事件返回值：" + json["content"].ToString() + "\n具体位置：" + LoggerHelper.GetCurSourceFileName() + "\n行数：" + LoggerHelper.GetLineNum());

                    //只获取content中的cardPrivateData包含的返回值信息
                    string resutle = json["content"].ToString();
                    json = JToken.Parse(resutle);
                    resutle = json["cardPrivateData"].ToString();

                    //只获取 cardPrivateData中包含的params参数
                    json = JToken.Parse(resutle);
                    resutle = json["params"].ToString();
                    json = JToken.Parse(resutle);
                    #endregion



                    Dictionary<string, string> cardDataCardParamMap = new Dictionary<string, string>
                    {
                        {"date",json["datetime"].ToString() },
                        {"outTrackId",cardID}
                    };
                    if (json["result"].ToString() == "1")
                    {

                        cardDataCardParamMap.Add("invalid", "11");
                        UpdateCardMessage.UdateCard(cardDataCardParamMap);

                    }
                    else
                    {
                        string text = "请按照模板回复替班人员姓名，“值班人员：XXX”";
                        cardDataCardParamMap.Add("wrong", "11");
                        UpdateCardMessage.UdateCard(cardDataCardParamMap);
                        RobotSendText.RobotSendMessage(userid, text);


                    }


                }
            }
            catch (Exception e)
            {

                LoggerHelper.Error("值班卡片回调函数错误信息：" + e.Message + "  具体信息：" + e.StackTrace);
            }
        }
        #endregion
    }
}