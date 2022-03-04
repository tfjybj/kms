using System;

namespace KmsService.Entity
{
    public class DutyInfoEntity
    {
        //自增id
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        //值班人员姓名
        private string dutyNameTwo;

        public string DutyNameTwo
        {
            get { return dutyNameTwo; }
            set { dutyNameTwo = value; }
        }

        //值班人员姓名
        private string dutyNameOne;

        public string DutyNameOne
        {
            get { return dutyNameOne; }
            set { dutyNameOne = value; }
        }

        //创建时间
        private DateTime createTime;

        public DateTime CreateTime
        {
            get { return createTime; }
            set { createTime = value; }
        }

        //值班日期
        private DateTime dutyDate;

        public DateTime DutyDate
        {
            get { return dutyDate; }
            set { dutyDate = value; }
        }

        //更新时间
        private DateTime updateTime;

        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }
    }
}