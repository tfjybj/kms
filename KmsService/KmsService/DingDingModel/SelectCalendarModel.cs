using System;

namespace KmsService.DingDingModel
{

    public class SelectCalendarModel
    {
        /// <summary>
        /// 日程id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 日程标题
        /// </summary>
        public string summary { get; set; }
        /// <summary>
        /// 日程描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 日程状态
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 日程开始时间
        /// </summary>
        public Start start { get; set; }
        /// <summary>
        /// 日程结束时间
        /// </summary>
        public End end { get; set; }
        /// <summary>
        /// 是否为全天日程
        /// </summary>
        public bool isAllDay { get; set; }
        /// <summary>
        /// 日程循环规则
        /// </summary>
        public Recurrence recurrence { get; set; }
        /// <summary>
        /// 参与人列表
        /// </summary>
        public Attendee[] attendees { get; set; }
        /// <summary>
        /// 组织者
        /// </summary>
        public Organizer organizer { get; set; }
        /// <summary>
        /// 日程地点相关信息
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// 重复日程的主日程id，非重复日程为空
        /// </summary>
        public string seriesMasterId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updateTime { get; set; }
        /// <summary>
        /// 线上会议
        /// </summary>
        public Onlinemeetinginfo onlineMeetingInfo { get; set; }
    }

    public class Start
    {
        public string date { get; set; }
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }

    public class End
    {
        public string date { get; set; }
        public DateTime dateTime { get; set; }
        public string timeZone { get; set; }
    }

    public class Recurrence
    {
        public Pattern pattern { get; set; }
        public Range range { get; set; }
    }

    public class Pattern
    {
        public string type { get; set; }
        public int dayOfMonth { get; set; }
        public string daysOfWeek { get; set; }
        public string index { get; set; }
        public int interval { get; set; }
    }

    public class Range
    {
        public string type { get; set; }
        public DateTime endDate { get; set; }
        public int numberOfOccurrences { get; set; }
    }

    public class Organizer
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string responseStatus { get; set; }
        public bool self { get; set; }
    }

    public class Location
    {
        public string displayName { get; set; }
    }

    public class Onlinemeetinginfo
    {
        public string type { get; set; }
        public string conferenceId { get; set; }
        public string url { get; set; }
    }

    public class Attendee
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string responseStatus { get; set; }
        public bool self { get; set; }
    }

}