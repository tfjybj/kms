using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace KmsService.DAL
{
    public class SelectCalendarIDDAL
    {
        public static string calendarID;

        /// <summary>
        /// 获取当前并且日程内容包含值班的日程ID
        /// </summary>
        /// <returns></returns>
        public string SelectCalendarID()
        {
            string sql = "select * from t_calendar where content Like '%值班%' and start_time Like @nowtime";
            MySqlParameter[] mySql = new MySqlParameter[]
            {
                new MySqlParameter("@nowtime",'%'+DateTime.Now.ToString("yyyy-MM-dd")+'%')
            };
            SQLHelper sqlhelper = new SQLHelper();
            DataTable result = sqlhelper.ExecuteQuery(sql, mySql, CommandType.Text);

            foreach (DataRow row in result.Rows)
            {
                calendarID = Convert.ToString(row["calendar_id"]);
            }
            return calendarID;
        }
    }
}