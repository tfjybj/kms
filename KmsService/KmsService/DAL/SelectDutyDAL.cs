using MySql.Data.MySqlClient;
using System;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
    public class SelectDutyDAL
    {
        public DutyInfoEntity SelectDuty()
        {
            string sql = "select duty_name_one,duty_name_two from t_duty_infor where duty_date=@nowtime";
            MySqlParameter[] mySql = new MySqlParameter[]
            {
                new MySqlParameter("@nowtime",DateTime.Now.ToString("yyyy-MM-dd"))
            };
            SQLHelper sqlHelper = new SQLHelper();
            DataTable name = sqlHelper.ExecuteQuery(sql, mySql, CommandType.Text);
            DutyInfoEntity dutyInfor = new DutyInfoEntity();
            foreach (DataRow row in name.Rows)
            {
                dutyInfor.DutyNameOne = row["duty_name_one"].ToString();
                dutyInfor.DutyNameTwo = row["duty_name_two"].ToString();
                dutyInfor.CreateTime = DateTime.Now;
            }
            return dutyInfor;
        }
    }
}