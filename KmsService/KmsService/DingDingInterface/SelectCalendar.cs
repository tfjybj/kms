using Newtonsoft.Json;
using System.Collections.Generic;
using KmsService.DingDingModel;
using KmsService.Log4;

namespace KmsService.DingDingInterface
{
    public class SelectCalendar
    {
        /// <summary>
        /// 获取单个日程详情
        /// </summary>
        /// <param name="userID">日程所属用户的userId</param>
        /// <param name="CalendarID">日程所属的日历id</param>
        /// <param name="eventID">日程id</param>
        public SelectCalendarModel SelectCalendarInfo(string userID, string CalendarID, string eventID)
        {
            GetUnionID getUnionID = new GetUnionID();

            //根据用户ID获取用户的unionID
            GetUnionIDModel userModel = getUnionID.GetDingDingUnionID(userID);
            string unionId = userModel.result.unionid;

            //url地址
            string url = string.Format("http://api.dingtalk.com/v1.0/calendar/users/{0}/calendars/{1}/events/{2}", unionId, CalendarID, eventID);
            HttpHelper helper = new HttpHelper();

            //获取token值
            string token = new AccessToken().GetAccessToken();
            //执行url
            string result = helper.HttpGet(url, token, 1);

            //将返回值反序列化， 保存审批详情
            SelectCalendarModel calendarModel = JsonConvert.DeserializeObject<SelectCalendarModel>(result);
            LoggerHelper.Info("调用钉钉获取单个日程详情的结果：" + result);

            return calendarModel;
        }
    }
}