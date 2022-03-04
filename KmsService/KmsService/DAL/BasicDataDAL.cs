/*
 * 创建人：王梦杰
 * 创建日期：2022年1月11日19:45:39
 * 描述：基本数据配置表D层操作类
 */

using System;
using System.Data;
using KmsService.Entity;
namespace KmsService.DAL
{
    public class BasicDataDAL
    {
        private SQLHelper sqlHelper;

        public BasicDataDAL()
        {
            sqlHelper = new SQLHelper();
        }

        /// <summary>
        /// 查询基本数据配置表全部信息
        /// </summary>
        /// <returns>基本数据配置表实体</returns>
        public BasicDataEntity SelectAllBasicData()
        {
            string sql = "select id,upper_time,lower_time,create_time,update_time from t_basicdata";
            DataTable table = sqlHelper.ExecuteQuery(sql, CommandType.Text);
            BasicDataEntity basicData = new BasicDataEntity();
            foreach (DataRow row in table.Rows)
            {
                basicData.ID = Convert.ToInt32(row["id"]);
                basicData.UpperTime = Convert.ToInt32(row["upper_time"]);
                basicData.LowerTime = Convert.ToInt32(row["lower_time"]);
                basicData.CreateTime = row["create_time"].ToString();
                basicData.UpdateTime = row["update_time"].ToString();
            }
            return basicData;
        }

        ///// <summary>
        ///// 更改用户每周会议室使用情况的推送时间
        ///// </summary>
        ///// <param name="weekPushTime">推送时间的cron表达式</param>
        ///// <returns>受影响行数</returns>
        //public int UpdateWeekPushTime(string weekPushTime)
        //{
        //    string sql = "update t_basicdata set week_push_time=@week_push_time";
        //    MySqlParameter[] sqlParameters = new MySqlParameter[] { new MySqlParameter("@week_push_time", weekPushTime) };
        //    return sqlHelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        //}
    }
}