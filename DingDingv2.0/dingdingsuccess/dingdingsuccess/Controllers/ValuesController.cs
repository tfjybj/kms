using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using dingdingsuccess.Log4;
using dingdingsuccess.CardMessageBLL;
using dingdingsuccess.DingDingInterface;
using dingdingsuccess.DingDingEntity;
using dingdingsuccess.LimitBLL;
namespace dingdingsuccess.Controllers
{
    public class ValuesController : ApiController
    {
        Logger log = Logger.GetLogger("class_name");




        /// <summary>
        /// 钉钉回调事件方法
        /// </summary>
        /// <param name="signature">消息体签名</param>
        /// <param name="timestamp">事件戳</param>
        /// <param name="nonce">随机字符串</param>
        /// <returns></returns>
        [HttpPost]
        public DingDingBackEntity DingDingCallBack(string signature, string timestamp, string nonce)
        {
            //这两句代码是为了接收body体中传入的加密json串
            Request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);
            string content = Request.Content.ReadAsStringAsync().Result;
            //反序列化json串拿去加密字符串
            JToken json = JToken.Parse(content);
            string ever = json["encrypt"].ToString();
            //实例化钉钉解密类构造参数为对应的 应用中的token、appkeys、AppKey值
            DingTalkEncryptor dingTalkEncryptor = new DingTalkEncryptor(GetConfig.GetConfigValue("basicData", "token"), GetConfig.GetConfigValue("basicData", "aes_key"), GetConfig.GetConfigValue("basicData", "Appkey"));
            //定义字符串接收解密后的值
            string text = dingTalkEncryptor.getDecryptMsg(signature, timestamp, nonce, ever);
            JToken jToken = JToken.Parse(text);
            //取出事件类型字段
            string EventType = jToken["EventType"].ToString();


            try
            {

                //判断事件类型是否是日程事件
                if ("calendar_event_change" == EventType)
                {
                    //反序列化，将解密后的值放入对象中
                    CalendarInfoEntity model = JsonConvert.DeserializeObject<CalendarInfoEntity>(text);
                    DingDingUnidnID dingUnidnID = new DingDingUnidnID();

                    //根据日程当中的unionid获取用户ID
                    string userid = dingUnidnID.unionid(model.UnionIdList[0].ToString());

                    DingCalendarDetails dingCalendar = new DingCalendarDetails();


                    //根据unionID和日程ID获取日程中有多少人
                    CalendarDetailsEntity calendarDetailsModel = dingCalendar.SelectCalendarInfo(model.UnionIdList[0].ToString(), model.CalendarEventId);


                    //判断日程事件是是否是创建型
                    if ("created" == model.ChangeType)
                    {
                        LoggerHelper.Info("日程事件类型ChangeType:" + model.ChangeType);
                        CalendarLimit calendar = new CalendarLimit();
                        LoggerHelper.Info("日程用户信息日程ID、钉ID：" + model.CalendarEventId + "、" + userid);
                        string roomname = calendar.IsCalendars(model.UnionIdList[0].ToString(), model.CalendarEventId,userid);
                        
                        LoggerHelper.Info("推送房间名称:" + roomname);
                        //判断日程中的人数是否符合发送会议预约链接
                        if (roomname != "false")
                        {
                            string usedtime = calendarDetailsModel.start.dateTime.ToString() + "-" + calendarDetailsModel.end.dateTime.ToString();
                            //传入用户ID、日程id、房间名称、使用时长
                            new SendCardMessage().SendChoiceRoom(roomname, usedtime, model.CalendarEventId, userid);
                        }

                    }
                }

            }
            catch (System.Exception e)
            {
                LoggerHelper.Error("钉钉事件订阅回调接口错误信息：" + e.Message + "  具体信息：" + e.StackTrace);
                 
            }





            //将json串中的指定值取出           
            var msg = dingTalkEncryptor.getEncryptedMap("success");
            DingDingBackEntity back = new DingDingBackEntity();
            back.msg_signature = msg["msg_signature"];
            back.encrypt = msg["encrypt"];
            back.timeStamp = msg["timeStamp"];
            back.nonce = msg["nonce"];
            // string backmsg = JsonConvert.SerializeObject(msg);
            return back;


        }


    }
}
