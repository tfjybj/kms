
using dingdingsuccess.DingDingEntity;
using Newtonsoft.Json;
using dingdingsuccess.Log4;



namespace dingdingsuccess.DingDingInterface
{
    /// <summary>
    /// 查询当个日程详情类
    /// </summary>
    public class DingCalendarDetails
    {
        /// <summary>
        /// 获取单个日程详情
        /// </summary>
        /// <param name="unionId">日程所属用户的unionId</param>
        /// <param name="eventID">日程id</param>
        public CalendarDetailsEntity SelectCalendarInfo(string unionId,string eventID)
        {
            string CalendarID = "primary";
            LoggerHelper.Info("具体获取单个日程详情接口参数：" + unionId);
            //url地址
            string url = string.Format("http://api.dingtalk.com/v1.0/calendar/users/{0}/calendars/{1}/events/{2}", unionId, CalendarID, eventID);
            HttpHelper helper = new HttpHelper();

            //获取token值
            string token = AccessToken.GetAccessToken();
            //执行url 
            string result = helper.HttpGet(url, token,1);
            //获取单个审批详情

            //将返回值反序列化， 保存审批详情
            CalendarDetailsEntity calendarModel = JsonConvert.DeserializeObject<CalendarDetailsEntity>(result);
            
            
            return calendarModel;
        }
    }
}