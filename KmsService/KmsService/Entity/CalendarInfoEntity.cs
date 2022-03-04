namespace KmsService.Entity
{
    /// <summary>
    /// 日程信息表
    /// </summary>
    public class CalendarInfoEntity
    {
        //自增id
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        //日程ID
        private string calendarID;

        public string CalendarID
        {
            get { return calendarID; }
            set { calendarID = value; }
        }

        //内容
        private string content;

        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        //会议开始时间
        private string starTime;

        public string StartTime
        {
            get { return starTime; }
            set { starTime = value; }
        }

        //会议结束时间
        private string endTime;

        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        //钥匙归还时间
        private string returnTime;

        public string ReturnTime
        {
            get { return returnTime; }
            set { returnTime = value; }
        }

        //房间ID
        private string roomName;

        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }

        //参会人
        private string attendCount;

        public string AttendCount
        {
            get { return attendCount; }
            set { attendCount = value; }
        }

        //组织人
        private string organizer;

        public string Organizer
        {
            get { return organizer; }
            set { organizer = value; }
        }

        //组织人dingid
        private string organizerID;

        public string OrganizerID
        {
            get { return organizerID; }
            set { organizerID = value; }
        }

        //审批人
        private string approver;

        public string Approver
        {
            get { return approver; }
            set { approver = value; }
        }

        //抄送人
        private string sendPeople;

        public string SendPeople
        {
            get { return sendPeople; }
            set { sendPeople = value; }
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

        //会议开始时间状态
        private int isStart;

        public int IsStart
        {
            get { return isStart; }
            set { isStart = value; }
        }

        //会议结束时间状态
        private int isEnd;

        public int IsEnd
        {
            get { return isEnd; }
            set { isEnd = value; }
        }

        private string outTrackID;

        public string OutTrackID
        {
            get { return outTrackID; }
            set { outTrackID = value; }
        }
        //会议结束时间
        private string getTime;

        public string GetTime
        {
            get { return getTime; }
            set { getTime = value; }
        }

        //日程作废
        private int calendarIsVoid;
        public int CalendarIsVoid
        {
            get { return calendarIsVoid; }
            set { calendarIsVoid = value; }
        }

        //审批ID
        private string approveID;
        public string ApproveID
        {
            get { return approveID; }
            set { approveID = value; }
        }
    }
}