/*
 * 创建人：武梓龙
 * 创建时间：2022年1月26日09点06分
 * 描述：更新值班人员姓名类
 */

using MySql.Data.MySqlClient;
using System;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
    public class UpdateDutyNameDAL
    {
        /// <summary>
        /// 更新值班人员姓名
        /// </summary>
        /// <param name="OldDutyName">值班人员</param>
        /// <param name="NewDutyName">代替值班人员</param>
        /// <returns></returns>
        public int UpdateDutyName(string OldDutyName, string NewDutyName)
        {
            string dutyname = "";
            string sql = "select * from t_duty_infor where duty_date=@nowtime";
            MySqlParameter[] mySql = new MySqlParameter[]
            {
                new MySqlParameter("@nowtime",DateTime.Now.ToString("yyyy-MM-dd")),
            };
            SQLHelper sqlHelper = new SQLHelper();
            DataTable data = sqlHelper.ExecuteQuery(sql, mySql, CommandType.Text);
            DutyInfoEntity dutyInfo = new DutyInfoEntity();
            foreach (DataRow row in data.Rows)
            {
                dutyInfo.ID = Convert.ToInt32(row["id"]);
                dutyInfo.DutyNameOne = row["duty_name_one"].ToString();
                dutyInfo.DutyNameTwo = row["duty_name_two"].ToString();
                dutyInfo.DutyDate = Convert.ToDateTime(row["duty_date"]);
            }

            if (dutyInfo.DutyNameOne == OldDutyName)
            {
                dutyname = "duty_name_one";
            }
            else
            {
                dutyname = "duty_name_two";
            }
            string sql2 = "update t_duty_infor set " + @dutyname + " =@newdutyname where duty_date=@nowtime";
            MySqlParameter[] sqlParameter = new MySqlParameter[]
            {
                new MySqlParameter("@dutyname",dutyname),
                new MySqlParameter("@newdutyname",NewDutyName),
                new MySqlParameter("@nowtime",DateTime.Now.ToString("yyyy-MM-dd"))
            };
            return sqlHelper.ExecuteNonQuery(sql2, sqlParameter, CommandType.Text);
        }
    }
}