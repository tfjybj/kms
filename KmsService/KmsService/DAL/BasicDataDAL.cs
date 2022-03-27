/*
 * 创建人：王梦杰
 * 创建日期：2022年1月11日19:45:39
 * 描述：基本数据配置表D层操作类
 */

using System;
using System.Collections.Generic;
using System.Data;
using KmsService.Entity;
using MySql;
using MySql.Data.MySqlClient;

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
        public BasicDataEntity SelectAllBasicData(string roomName)
        {
            string sql = "select room_name,before_take_key,after_return_key,approver,approver_id,min_use_number,upper_time,lower_time,create_time,update_time from t_basicdata where room_name=@roomName";
            MySqlParameter[] mySqls = new MySqlParameter[] 
            {
                new MySqlParameter("@roomName",roomName)
            };
            DataTable table = sqlHelper.ExecuteQuery(sql,mySqls, CommandType.Text);
            BasicDataEntity basicData = new BasicDataEntity();
            foreach (DataRow row in table.Rows)
            {
                basicData.RoomName = row["room_name"].ToString();
                basicData.BeforeTakeKey = Convert.ToInt32(row["before_take_key"]);
                basicData.AfterReturnKey = Convert.ToInt32(row["after_return_key"]);
                basicData.Approver = row["approver"].ToString();
                basicData.ApproverID = row["approver_id"].ToString();
                basicData.MinUseNumber = Convert.ToInt32(row["min_use_number"]);
                basicData.UpperTime = Convert.ToInt32(row["upper_time"]);
                basicData.LowerTime = Convert.ToInt32(row["lower_time"]);
                basicData.CreateTime = row["create_time"].ToString();
                basicData.UpdateTime = row["update_time"].ToString();
            }
            return basicData;
        }

        /// <summary>
        /// 查询会议室最小使用人数
        /// </summary>
        /// <returns>集合</returns>
        public List<string> SelectMinUseNumber()
        {
            string sql = "select DISTINCT min_use_number from t_basicdata where min_use_number is not NULL";
            SQLHelper helper = new SQLHelper();
            DataTable roomPeopleTable = helper.ExecuteQuery(sql, CommandType.Text);
            List<string> roomPeopleList = new List<string>();
            foreach (DataRow row in roomPeopleTable.Rows)
            {
                roomPeopleList.Add(row["min_use_number"].ToString());
            }

            return roomPeopleList;
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