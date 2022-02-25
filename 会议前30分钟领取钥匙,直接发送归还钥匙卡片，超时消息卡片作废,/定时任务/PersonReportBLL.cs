


namespace 定时任务
{
    public class PersonReportBAL
    {
       
       PersonReportDAL prdal=new PersonReportDAL();
        /// <summary>
        /// 申请人的一周组织会议的次数
        /// </summary>
        /// <param name="ddID">申请人的钉钉id</param>
        /// <returns>申请人的一周组织会议的次数</returns>
        public string  UsageTimes(string dd)
        {
          
            string times;
            times= prdal.UsageTimes(dd);
            return times;

        }

        /// <summary>
        /// 申请人的一周会议使用最多次数的教室
        /// <param name="ddID">申请人的钉钉id</param>
        /// <returns></returns>
        public string  MaxUsedRoom(string dd)
        {
            
            string times;
            times = prdal.MaxUsedRoom(dd);
            return times;
        }


        /// <summary>
        /// 获取时间段最多的一条数据
        /// </summary>
        /// <param name="ddID">申请会议人的id</param>
        /// <returns></returns>
        public string  MaxStartTime(string dd)
        {
            string times;
            times = prdal.MaxStartTime(dd);
            return times;
        }


       
       

        
    }
}