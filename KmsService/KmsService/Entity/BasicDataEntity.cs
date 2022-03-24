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
        /// 教师名称
        /// </summary>
        private string roomName;
        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }

        /// <summary>
        /// 至少使用人数
        /// </summary>
        private int minUseNumber;
        public int MinUseNumber
        {
            get {return minUseNumber; }
            set { minUseNumber = value; }
        }

        /// <summary>
        /// 会议开始前*分钟取钥匙
        /// </summary>
        private int beforeTakeKey;
        public int BeforeTakeKey
        {
            get { return beforeTakeKey; }
            set { beforeTakeKey = value; }
        }

        /// <summary>
        /// 会议结束前*分钟还钥匙
        /// </summary>
        private int afterReturnKey;
        public int AfterReturnKey
        {
            get { return afterReturnKey; }
            set { afterReturnKey = value; }
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
        /// 审批人
        /// </summary>
        private string approver;
        public string Approver
        {
            get { return approver; }
            set { approver = value; }
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
    }
}