namespace KmsService.Entity
{
    /// <summary>
    /// 房间信息表
    /// </summary>
    public class RoomInfoEntity
    {
        //自增id
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        //锁编号
        private string lockNumber;

        public string LockNumber
        {
            get { return lockNumber; }
            set { lockNumber = value; }
        }

        //教室名称
        private string roomName;

        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }

        //锁的状态
        private string lockState;

        public string LockState
        {
            get { return lockState; }
            set { lockState = value; }
        }

        //开锁前多少分钟可以取钥匙
        private int frontMin;

        public int FrontMin
        {
            get { return frontMin; }
            set { frontMin = value; }
        }

        //教室最少使用人数
        private int minUseNumber;

        public int MinUseNumber
        {
            get { return minUseNumber; }
            set { minUseNumber = value; }
        }

        //创建时间
        private string createTime;

        public string CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        //更新时间
        private string updateTime;

        public string UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
    }
}