/*
 * 创建人：盖鹏军
 * 时间：2022年2月23日16点54分
 * 描述：审批事件实体
 */
using System.Collections.Generic;
namespace dingdingsuccess
{

    public class CalendarInfoEntity
    {
        /// <summary>
        /// 日程组织者所属的主企业
        /// </summary>
        public string CorpId { get; set; }
        /// <summary>
        /// 事件类型：calendar_event_change：用户日程发生变更事件。
        /// </summary>
        public string EventType { get; set; }
        /// <summary>
        /// 事件发生时间。
        /// </summary>
        public long EventTime { get; set; }
        /// <summary>
        /// 发生变更的日程id。
        /// </summary>
        public string CalendarEventId { get; set; }
        /// <summary>
        /// 本次日程变更影响的用户unionId列表。
        /// </summary>
        public List< string> UnionIdList { get; set; }
        /// <summary>
        /// 业务类型：
        ///created：创建
        ///updated：更新
        ///cancelled：取消
        ///deleteView：用户在自己本地删除日程
        /// </summary>
        public string ChangeType { get; set; }
        /// <summary>
        /// 日历Id。
        /// </summary>
        public string CalendarId { get; set; }
    }

}