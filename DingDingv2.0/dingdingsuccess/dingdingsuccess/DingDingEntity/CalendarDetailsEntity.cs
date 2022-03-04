/*
 * 创建人：盖鹏军
 * 时间：2022年2月23日16点54分
 * 描述：日程详情实体
 */
using System;

namespace dingdingsuccess.DingDingEntity
{
    #region 日程全部信息
    /// <summary>
    /// 日程详情实体
    /// </summary>
    public class CalendarDetailsEntity
    {
        public Attendee[] attendees { get; set; }
        public DateTime createTime { get; set; }
        public string description { get; set; }
        public End end { get; set; }
        public string id { get; set; }
        public bool isAllDay { get; set; }
        public Onlinemeetinginfo onlineMeetingInfo { get; set; }
        public Organizer organizer { get; set; }
        public Reminder[] reminders { get; set; }
        public string requestId { get; set; }
        public Start start { get; set; }
        public string status { get; set; }
        public string summary { get; set; }
        public DateTime updateTime { get; set; }

    }
    ///// <summary>
    ///// 线上会议
    ///// </summary>
    //public Onlinemeetinginfo onlineMeetingInfo { get; set; }

    public class End
    {
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }
    public class Onlinemeetinginfo
    {
        public string conferenceId { get; set; }
        public Extrainfo extraInfo { get; set; }
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Extrainfo
    {
        public string roomCode { get; set; }
    }


    public class Organizer
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public bool self { get; set; }
    }

    public class Start
    {
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }

    public class Attendee
    {
        public string displayName { get; set; }
        public string id { get; set; }
        public string responseStatus { get; set; }
        public bool self { get; set; }
    }

    public class Reminder
    {
        public string method { get; set; }
        public string minutes { get; set; }
    }

    #endregion

}