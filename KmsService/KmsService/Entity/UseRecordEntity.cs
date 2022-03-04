using System;

namespace KmsService.Entity
{
    /// <summary>
    /// 实体类 t_use_record_info, 此类请勿动，以方便表字段更改时重新生成覆盖
    /// </summary>

    public class UseRecordEntity
    {
        public UseRecordEntity()
        { }

        /// <summary>
        /// 构造函数 t_use_record_info
        /// </summary>
        /// <param name="id">自增id主键</param>
        /// <param name="room_name">会议室名称</param>
        /// <param name="organizer">会议组织人</param>
        /// <param name="content">会议内容</param>
        /// <param name="start_datetime">开始时间</param>
        /// <param name="end_datetime">结束时间</param>
        /// <param name="approveID">审批实例ID</param>
        /// <param name="organizer_dingID">申请人dingID</param>
        public UseRecordEntity(string room_name, string organizer, string content, DateTime start_datetime, DateTime end_datetime, string approveID, string organizerID)
        {
            this.room_name = room_name;
            this.organizer = organizer;
            this.content = content;
            this.start_datetime = start_datetime;
            this.end_datetime = end_datetime;
            this.approvrID = approveID;
            this.organizerID = organizerID;
        }

        #region 实体属性

        /// <summary>
        /// 会议室名称
        /// </summary>
        public string room_name { get; set; }

        /// <summary>
        /// 会议组织人
        /// </summary>
        public string organizer { get; set; }

        /// <summary>
        /// 会议内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime start_datetime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime end_datetime { get; set; }

        /// <summary>
        /// 申请人dingID
        /// </summary>
        public string organizerID { get; set; }

        /// <summary>
        /// 审批实例ID
        /// </summary>
        public string approvrID { get; set; }

        #endregion 实体属性
    }
}