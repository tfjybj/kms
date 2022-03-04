/*
 *创建人：王梦杰
 *创建时间：2022年1月5日08:34:35
 *描述：推送用户每周会议室使用情况
 */

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace KmsService.DAL
{
    /// <summary>
    /// 获取用户一周的会议室使用情况
    /// </summary>
    public class PersonReportDAL
    {
        private SQLHelper sqlhelper = new SQLHelper();

        /// <summary>
        /// 查询会议组织人一周的会议组织次数
        /// </summary>
        /// <param name="ddID">组织人的钉钉id</param>
        /// <returns>会议组织次数</returns>
        public string UsageTimes(string ddID)
        {
            string sql = "SELECT COUNT(organizer_id) as num from t_calendar where organizer_id=@organizerID and DATE_SUB(curdate(),INTERVAL 7 DAY) <=date(start_time)";

            MySqlParameter[] mySql = new MySqlParameter[]
            {
                    new MySqlParameter("@organizerID",ddID),//把集合中的一个ddid传入并循环其他的钉钉id，
            };

            //执行SQL语句
            DataTable result = sqlhelper.ExecuteQuery(sql, mySql, CommandType.Text);

            string number = null;
            foreach (DataRow row in result.Rows)
            {
                number = row["num"].ToString();
            }
            return number;
        }

        /// <summary>
        /// 使用最多次数的教室
        /// </summary>
        /// <param name="user">申请人的钉钉ID</param>
        /// <returns>会议室名称</returns>
        public string MaxUsedRoom(string ddID)
        {
            //定义一条SQL语句
            string sql = "select room_name,count(1) as number from t_calendar where organizer_id=@organizerID and DATE_SUB(curdate(),INTERVAL 7 DAY) <=date(start_time)  GROUP BY room_name ORDER BY count(1) desc";
            MySqlParameter[] mySql = new MySqlParameter[]
            {
              new MySqlParameter("@organizerID",ddID),
            };

            //执行SQL
            DataTable result = sqlhelper.ExecuteQuery(sql, mySql, CommandType.Text);

            string roomName = null;

            //把第一条数据取出来
            foreach (DataRow row in result.Rows)
            {
                roomName = row["room_name"].ToString();
                break;
            }
            return roomName;
        }

        //
        /// <summary>
        /// 使用最多次数的时间段
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>使用次数最多的时间段</returns>
        public string MaxStartTime(string ddID)
        {
            //先对两个字段进行拼接CONCAT函数，然后计数count，再分组排序 GROUP BY ORDER BY 取第一行数据limit 1
            string sql = "SELECT count(*) as max, CONCAT(DATE_FORMAT(start_time,'%H:%i:%s') ,'-',DATE_FORMAT(end_time,'%H:%i:%s')) as connectTime  from t_calendar where organizer_id = @organizerID GROUP BY connectTime ORDER BY max DESC limit 0,1";
            MySqlParameter[] mySql = new MySqlParameter[]
            {
                new MySqlParameter("@organizerID",ddID),
            };
            //执行SQL
            DataTable result = sqlhelper.ExecuteQuery(sql, mySql, CommandType.Text);
            string NumberStartTime = "00:00-00:00";
            foreach (DataRow row in result.Rows)
            {
                NumberStartTime = Convert.ToString(row["connectTime"]);
            }

            return NumberStartTime;
        }

        /// <summary>
        /// 获取数据库中所有的钉钉id
        /// </summary>
        /// <returns>所有用户的钉钉ID</returns>
        public List<string> AllddID()
        {
            string sql = "select distinct organizer_id from t_calendar";//把数据库中的所有的钉钉id拿到

            DataTable result = sqlhelper.ExecuteQuery(sql, CommandType.Text);
            //把所有获取到的钉钉id放到一个集合里面

            List<string> dd = new List<string>();
            foreach (DataRow row in result.Rows)
            {
                dd.Add(row["organizer_id"].ToString());
            }
            return dd;
        }
    }
}