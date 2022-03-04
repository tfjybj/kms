/*
 * 创建人：王梦杰
 * 创建时间：2022年1月11日19:35:15
 * 描述：基本数据配置实体类
 */

namespace KmsService.Entity
{
    /// <summary>
    /// 基本数据配置实体
    /// </summary>
    public class BasicDataEntity
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 会议室使用时间下限
        /// </summary>
        private int upperTime;

        public int UpperTime
        {
            get { return upperTime; }
            set { upperTime = value; }
        }

        /// <summary>
        /// 会议室使用时间上限
        /// </summary>
        private int lowerTime;

        public int LowerTime
        {
            get { return lowerTime; }
            set { lowerTime = value; }
        }

        /// <summary>
        /// 创建时间
        /// </summary>
        private string createTime;

        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        private string updateTime;

        public string UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        ///// <summary>
        ///// 用户每周会议室使用情况的推送时间
        ///// </summary>
        //private string weekPushTime;
        //public string WeekPushTime
        //{
        //    get { return weekPushTime; }
        //    set { weekPushTime = value; }
        //}
    }
}