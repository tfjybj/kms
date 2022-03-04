using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KmsService.Entity
{
    public class ErrorInfoEntity
    {
        //组织人
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //会议时间
        private string time;

        public string Time
        {
            get { return time; }
            set { time = value; }
        }


        //会议室名称
        private string  roomName;

        public string  RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }

        //锁号
        private string  lockNumber;

        public string  LockNumber
        {
            get { return lockNumber; }
            set { lockNumber = value; }
        }
    }
}