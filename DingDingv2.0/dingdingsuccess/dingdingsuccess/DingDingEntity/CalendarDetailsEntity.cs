using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dingdingsuccess.DingDingEntity
{
    /// <summary>
    /// 日程详情实体
    /// </summary>
    //public class CalendarDetailsEntity
    //{
    //    / <summary>
    //    / 参与人列表
    //    / </summary>
    //    public List<Attendee> attendees { get; set; }
    //    / <summary>
    //    / 创建时间
    //    / </summary>
    //    public DateTime createTime { get; set; }
    //    / <summary>
    //    / 日程结束时间
    //    / </summary>
    //    public End end { get; set; }
    //    / <summary>
    //    / 日程id
    //    / </summary>
    //    public string id { get; set; }
    //    / <summary>
    //    / 是否为全天日程
    //    / </summary>
    //    public bool isAllDay { get; set; }
    //    / <summary>
    //    / 日程地点相关信息
    //    / </summary>
    //    public Location location { get; set; }

    //    / <summary>
    //    / 线上会议
    //    / </summary>
    //    public Onlinemeetinginfo onlineMeetingInfo { get; set; }
    //    / <summary>
    //    / 组织者
    //    / </summary>
    //    public Organizer organizer { get; set; }
    //    / <summary>
    //    / 日程提醒
    //    / </summary>
    //    public Reminder[] reminders { get; set; }

    //    public string requestId { get; set; }
    //    / <summary>
    //    / 日程开始时间
    //    / </summary>
    //    public Start start { get; set; }
    //    / <summary>
    //    / 日程状态
    //    / </summary>
    //    public string status { get; set; }
    //    / <summary>
    //    / 日程标题
    //    / </summary>
    //    public string summary { get; set; }
    //    / <summary>
    //    / 更新时间
    //    / </summary>
    //    public DateTime updateTime { get; set; }
    //}

    //public class End
    //{
    //    public DateTime dateTime { get; set; }
    //    public string timeZone { get; set; }
    //}

    //public class Location
    //{
    //    public string displayName { get; set; }
    //}

    //public class Organizer
    //{
    //    public string displayName { get; set; }
    //    public string id { get; set; }
    //    public bool self { get; set; }
    //}

    //public class Start
    //{
    //    / <summary>
    //    / 日程开始日期，格式：yyyy-MM-dd。
    //    / </summary>
    //    public DateTime dateTime { get; set; }
    //    public string timeZone { get; set; }
    //}

    //public class Attendee
    //{

    //    public string displayName { get; set; }
    //    public string id { get; set; }
    //    public string responseStatus { get; set; }
    //    public bool self { get; set; }
    //}
    //public class Onlinemeetinginfo
    //{
    //    public string conferenceId { get; set; }
    //    public Extrainfo extraInfo { get; set; }
    //    public string type { get; set; }
    //    public string url { get; set; }
    //}
    //public class Extrainfo
    //{
    //    public string roomCode { get; set; }
    //}

    //public class Reminder
    //{
    //    public string method { get; set; }
    //    public string minutes { get; set; }
    //}

    #region 日程全部信息

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