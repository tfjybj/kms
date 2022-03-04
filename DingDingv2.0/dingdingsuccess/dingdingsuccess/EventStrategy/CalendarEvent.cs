using dingdingsuccess.CardMessageBLL;
using dingdingsuccess.DingDingEntity;
using dingdingsuccess.DingDingInterface;
using dingdingsuccess.LimitBLL;
using dingdingsuccess.Log4;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess.EventStrategy
{
    /// <summary>
    /// 日程事件
    /// </summary>
    public class CalendarEvent : EventType
    {
        public override void ActEvent(string eventContent)
        {

            //反序列化，将解密后的值放入对象中
            CalendarInfoEntity model = JsonConvert.DeserializeObject<CalendarInfoEntity>(eventContent);
            DingDingUnidnID dingUnidnID = new DingDingUnidnID();

            //根据日程当中的unionid获取用户ID
            string userID = dingUnidnID.unionid(model.UnionIdList[0].ToString());

            DingCalendarDetails dingCalendar = new DingCalendarDetails();


            //根据unionID和日程ID获取日程中有多少人
            CalendarDetailsEntity calendarDetailsModel = dingCalendar.SelectCalendarInfo(model.UnionIdList[0].ToString(), model.CalendarEventId);


            //判断日程事件是是否是创建型
            if ("created" == model.ChangeType)
            {
                LoggerHelper.Info("日程事件类型ChangeType:" + model.ChangeType+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                CalendarLimit calendar = new CalendarLimit();
                LoggerHelper.Info("日程事件用户信息日程ID、钉ID：" + model.CalendarEventId + "、" + userID+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                string roomname = calendar.IsCalendars(model.UnionIdList[0].ToString(), model.CalendarEventId, userID);

                LoggerHelper.Info("推送房间名称:" + roomname+"\n具体位置："+LoggerHelper.GetCurSourceFileName()+"\n行数："+LoggerHelper.GetLineNum());
                //判断日程中的人数是否符合发送会议预约链接
                if (roomname != "false")
                {
                    string usedTime = calendarDetailsModel.start.dateTime.ToString() + "-" + calendarDetailsModel.end.dateTime.ToString();
                    //传入用户ID、日程id、房间名称、使用时长
                    new SendCardMessage().SendChoiceRoom(roomname, usedTime, model.CalendarEventId, userID);
                }

            }
        }
    }
}
