/*
 * 创建人：盖鹏军
 * 创建时间：2022年1月24日15点00分
 * 描述：对管理员记录表进行操作
 */
using System.Data;
using MySql.Data.MySqlClient;
using KmsService.Entity;
using System;
using System.Collections.Generic;

namespace KmsService.DAL
{
    public class ManagerRecordDAL
    {
        private SQLHelper sqlHelper;
        public ManagerRecordDAL()
        {
            sqlHelper = new SQLHelper();
        }

        /// <summary>
        /// 查询管理员表某个会议室正在使用的管理员
        /// </summary>
        /// <param name="roomName">房间名称</param>
        /// <returns>管理员实体集合</returns>
        public List<ManagerRecordEntity> SelectSameTimeManagerUse(string roomName)
        {
            string sql = "SELECT manager_name,key_name,user_id,get_time,is_return_key,is_cancel FROM t_manager_record WHERE key_name=@roomName AND get_time is NOT null and to_days(create_time)=to_days(now()) and is_return_key = @isReturnKey and is_cancel = @isCancel";
            MySqlParameter[] mySqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@roomName",roomName),
                new MySqlParameter("@isReturnKey","0"),
                new MySqlParameter("@isCancel","0")
            };
            List<ManagerRecordEntity> managerRecords = new List<ManagerRecordEntity>();
            DataTable managerRecordDataTable = sqlHelper.ExecuteQuery(sql,mySqlParameters,CommandType.Text);
            foreach (DataRow row in managerRecordDataTable.Rows)
            {
                managerRecords.Add(new ManagerRecordEntity() 
                { 
                    manager_name=row["manager_name"].ToString(),
                    key_name = row["key_name"].ToString(),
                    user_id=row["user_id"].ToString(),
                    get_time = row["get_time"].ToString(),
                    is_return_key = row["is_return_key"].ToString(),
                    is_cancel = row["is_cancel"].ToString()
                });
            }
            return managerRecords;
        }

        /// <summary>
        /// 插入领取记录
        /// </summary>
        /// <param name="managerRecord"></param>
        /// <returns></returns>
        public int InsertRecord(ManagerRecordEntity managerRecord)
        {
            string sql = "insert into t_manager_record (manager_name,key_name,user_id,is_cancel,get_out_track_id,create_time) values(@manager_name,@key_name,@userID,@is_cancel,@get_out_track_id,@createTime)";

            MySqlParameter[] mysql = new MySqlParameter[] {
                new MySqlParameter("@manager_name",managerRecord.manager_name),
                new MySqlParameter("@key_name",managerRecord.key_name),
                new MySqlParameter("@userID",managerRecord.user_id),
                new MySqlParameter("@is_cancel",managerRecord.is_cancel),
                new MySqlParameter("@get_out_track_id",managerRecord.get_out_track_id),
                new MySqlParameter("@createTime",DateTime.Now)
            };
            return sqlHelper.ExecuteNonQuery(sql, mysql, CommandType.Text);

        }

        /// <summary>
        /// 作废卡片，更新卡片是否可使用记录
        /// </summary>
        /// <param name="cardID">领取钥匙卡片ID</param>
        /// <returns></returns>
        public int UpdateCancelRecord(string cardID)
        {
            string sql = "UPDATE t_manager_record SET is_cancel='1' WHERE get_out_track_id=@cardID";
            MySqlParameter[] mysql = new MySqlParameter[] { new MySqlParameter("@cardID", cardID) };
            return sqlHelper.ExecuteNonQuery(sql, mysql, CommandType.Text);
        }

        /// <summary>
        /// 领取钥匙更新记录
        /// </summary>
        /// <param name="cardID">领取钥匙卡片ID</param>
        /// <param name="returncardid">归还钥匙卡片ID</param>
        /// <returns></returns>
        public int UpdateGetKey(string cardID, string returncardid)
        {

            string sql = "UPDATE t_manager_record SET is_return_key='0',get_time=@time,return_out_track_id=@returncardID,is_cancel='0' WHERE get_out_track_id=@cardID";
            MySqlParameter[] mysql = new MySqlParameter[] { new MySqlParameter("@cardID", cardID), new MySqlParameter("@time", DateTime.Now), new MySqlParameter("@returncardID", returncardid) };
            return sqlHelper.ExecuteNonQuery(sql, mysql, CommandType.Text);




        }


        /// <summary>
        /// 管理员归还钥匙
        /// </summary>
        /// <param name="returncardid">归还卡片ID</param>
        /// <returns></returns>
        public int UpdateReturnKey(string returncardid)
        {
            string sql = "UPDATE t_manager_record SET is_return_key='1',update_time=@update WHERE return_out_track_id=@cardID";
            MySqlParameter[] mysql = new MySqlParameter[] { new MySqlParameter("@cardID", returncardid), new MySqlParameter("@update", DateTime.Now) };
            return sqlHelper.ExecuteNonQuery(sql, mysql, CommandType.Text);
        }

        /// <summary>
        /// 查询未归还钥匙信息，根据领取钥匙卡片id查询
        /// </summary>
        /// <param name="cardID">领取钥匙卡片</param>
        /// <returns></returns>
        public ManagerRecordEntity SelectGetRecord(string cardID)
        {
            string sql = "select * from t_manager_record  WHERE get_out_track_id=@cardID";
            MySqlParameter[] mySql = new MySqlParameter[] { new MySqlParameter("@cardID", cardID) };
            DataTable table = sqlHelper.ExecuteQuery(sql, mySql, CommandType.Text);
            ManagerRecordEntity record = new ManagerRecordEntity();
            foreach (DataRow item in table.Rows)
            {
                record.manager_name = item["manager_name"].ToString();
                record.key_name = item["key_name"].ToString();
                record.get_out_track_id = item["get_out_track_id"].ToString();
                record.get_time = item["get_time"].ToString();
                record.user_id = item["user_id"].ToString();
            }
            return record;
        }


        /// <summary>
        /// 查询归还钥匙的钥匙名称
        /// </summary>
        /// <param name="returnCardID">归还钥匙卡片ID</param>
        /// <returns>钥匙名称</returns>
        public ManagerRecordEntity SelectReturnKey(string returnCardID)
        {
           
            string sql = "select key_name,user_id from t_manager_record where return_out_track_id=@returnCardID and is_return_key='0'";
            MySqlParameter[] mysql = new MySqlParameter[] { new MySqlParameter("@returnCardID", returnCardID) };
            DataTable dataTable = sqlHelper.ExecuteQuery(sql, mysql, CommandType.Text);
            ManagerRecordEntity record = new ManagerRecordEntity();
            foreach (DataRow item in dataTable.Rows)
            {
                record.key_name = item["key_name"].ToString();
                record.user_id = item["user_id"].ToString();
            }
            return record;
        }

        /// <summary>
        /// 查询正在使用中的记录
        /// </summary>
        /// <param name="roomName">会议室名称</param>
        /// <returns></returns>
        public ManagerRecordEntity SelectOccupiedRecord(string roomName)
        {
            string sql = "select * from t_manager_record where  key_name=@roomName  and to_days(get_time)=to_days(now()) and is_return_key=0";
            MySqlParameter[] mysql = new MySqlParameter[] { new MySqlParameter("@roomName", roomName) };
            DataTable dataTable = sqlHelper.ExecuteQuery(sql, mysql, CommandType.Text);
            ManagerRecordEntity Occupiedrecord = new ManagerRecordEntity();
            foreach (DataRow item in dataTable.Rows)
            {
                Occupiedrecord.key_name = item["key_name"].ToString();
                Occupiedrecord.user_id = item["user_id"].ToString();
                Occupiedrecord.manager_name=item["manager_name"].ToString();
                
            }
            return Occupiedrecord;
        }


    }
}