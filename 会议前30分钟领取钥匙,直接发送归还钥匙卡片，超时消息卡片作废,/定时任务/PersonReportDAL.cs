using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;


namespace 定时任务
{
    /*
     获取组织会议人一周之内的会议次数
    使用最多次数的教室
    使用最多次数的时间段
     */

    public class PersonReportDAL
    {
        SQLHelper sqlhelper=new SQLHelper();

        
        //查询会议组织人一周的会议组织次数
        /// <summary>
        /// 查询会议组织人一周的会议组织次数
        /// </summary>
        /// <param name="ddID">组织人的钉钉id</param>
        /// <returns></returns>
        public string  UsageTimes(string ddID )
        {
            
            string sql= " SELECT COUNT(organizer_id) as num from t_calendar where organizer_id=@organizerID and DATE_SUB(curdate(),INTERVAL 7 DAY) <=date(start_time)";

           // List<string> list = new List<string>();//实例化一个list集合

           //list= AllddID();//把查询到的数据放到list集合中

            
                MySqlParameter[] mySql = new MySqlParameter[]
                {
                    new MySqlParameter("@organizerID",ddID),//把集合中的一个ddid传入并循环其他的钉钉id，
                };

            

            DataTable result = sqlhelper.ExecuteQuery(sql,mySql, CommandType.Text);

            string  number="0";
            foreach (DataRow row in result.Rows)//获取查询到的次数
            {
                number =Convert.ToString( row["num"]);
            }
            return number;
        }

  

        /// <summary>
        /// 使用最多次数的教室
        /// </summary>
        /// <param name="user">申请人的钉钉ＩＤ</param>
        /// <returns></returns>
        public string  MaxUsedRoom(string ddID )
       {
            string sql= " SELECT COUNT(roomID) as num from t_calendar where organizer_id=@organizerID and DATE_SUB(curdate(),INTERVAL 7 DAY) <=date(startTime)";
            MySqlParameter[] mySql = new MySqlParameter[]
            {
              new MySqlParameter("@organizerID",ddID),
            };

            DataTable  result = sqlhelper.ExecuteQuery(sql, mySql, CommandType.Text);
            string   NumberRoom="0" ;
            foreach (DataRow row in result.Rows)
            {
                NumberRoom =Convert .ToString ( row["num"]);
            }
            return NumberRoom ;
        }

        //
        /// <summary>
        /// 使用最多次数的时间段  
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string   MaxStartTime(string ddID)
            {
         
            string sql= "SELECT count(*) as max, CONCAT(DATE_FORMAT(start_time,'%H:%i:%s') ,'-',DATE_FORMAT(end_time,'%H:%i:%s')) as connectTime  from t_calendar GROUP BY connectTime ORDER BY max DESC limit 0,1";//先对两个字段进行拼接CONCAT函数，然后计数count，再分组排序 GROUP BY ORDER BY 取第一行数据limit 1
            MySqlParameter[] mySql = new MySqlParameter[]
            {
                new MySqlParameter("@organizerID",ddID),
            };
            
            DataTable result=sqlhelper.ExecuteQuery(sql, mySql, CommandType.Text );
           string   NumberStartTime="00:00-00:00" ;
            foreach (DataRow row in result.Rows)
            {
                NumberStartTime = Convert.ToString (row["connectTime"]);
            }
            
            return NumberStartTime;

        }
/// <summary>
/// 使用最多的时间段 结束时间
/// </summary>
/// <param name="ddID"></param>
/// <returns></returns>
        //public int MaxEndTime(UseRecordEntity ddID)
        //{
        //    string sql = "select count(endTime) as num from t_calendar_info where organizerID=@organizerID and startTime(dd,datetime,getdate())<=7";
        //    MySqlParameter[] mySql = new MySqlParameter[]
        //        {
        //        new MySqlParameter("@organizerID", AllddID()),
        //};
        //    DataTable result = sqlhelper.ExecuteQuery(sql, mySql, CommandType.Text);
        //    int NumberEndTime = 0;
        //    foreach (DataRow  row in result.Rows)
        //    {
        //        NumberEndTime = Convert.ToInt32(row["num"]);
        //    };
        //    return NumberEndTime;
        //}

       /// <summary>
       /// 获取数据库中所有的钉钉id
       /// </summary>
       /// <returns></returns>
        public  List<string> AllddID()
        {
            string sql = "select distinct organizer_id from t_calendar";//把数据库中的所有的钉钉id拿到

            DataTable result = sqlhelper.ExecuteQuery(sql, CommandType.Text);
            //把所有获取到的钉钉id放到一个集合里面
           // List<string> dd = null;
            List<string> dd = new List<string>();
            foreach (DataRow row in result.Rows)
            {

                dd.Add(row["organizer_id"].ToString());
            }

            return dd;
        }
    }
}