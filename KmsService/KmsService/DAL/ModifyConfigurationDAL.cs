/*
 * 创建人：王梦杰
 * 创建日期：2022年4月1日10:02:37
 * 描述：修改配置信息D层操作类
 */
using KmsService.Entity;
using KmsService.Log4;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace KmsService.DAL
{
    public class ModifyConfigurationDAL
    {
        private SQLHelper sqlhelper = new SQLHelper();

        /// <summary>
        ///  取出所有教室信息
        /// </summary>
        /// <returns>数据表</returns>
        public DataTable SelectBasicData()
        {
            //try
            //{
            DataTable dt = new DataTable();
            string sql = "SELECT id,room_name,min_use_number,before_take_key,after_return_key,upper_time,lower_time,approver FROM t_basicdata";
            dt = sqlhelper.ExecuteQuery(sql, CommandType.Text);
            return dt;
            //}
            //catch (System.Exception ex)
            //{
            //    LoggerHelper.Error("取出所有教室信息" + ex.Message + "堆栈信息：" + ex.StackTrace);
            //}
        }

        /// <summary>
        /// 判断教室名称是否已经存在
        /// </summary>
        /// <param name="caName">教室名称</param>
        /// <returns>bool</returns>
        public bool RoomNameIsExists(string roomName)
        {
            bool flag = false;
            string sql = "select room_name from t_basicdata where room_name =@roomName";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@roomName",roomName)
            };
            DataTable dt = sqlhelper.ExecuteQuery(sql, sqlParameters, CommandType.Text);
            if (dt.Rows.Count > 0)

            {
                flag = true;
            }
            LoggerHelper.Info("判断教师名称是否已存在（True：已存在此教室名称，false：不存在）：" + flag);
            return flag;

        }

        /// <summary>
        /// 判断是否存在此教室id
        /// </summary>
        /// <param name="caName">教室名称</param>
        /// <returns>true:存在此id的教室；false：不存在此i的教室</returns>
        public bool RoomIdIsExists(string roomId)
        {
            bool flag = false;
            string sql = "select id,room_name from t_basicdata where id =@roomId";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@roomId",roomId)
            };
            DataTable dt = sqlhelper.ExecuteQuery(sql, sqlParameters, CommandType.Text);
            if (dt.Rows.Count > 0)

            {
                flag = true;
            }
            return flag;

        }

        /// <summary>
        /// 更新基本数据配置表
        /// </summary>
        /// <param name="basicDataEntity">基本数据配置实体</param>
        /// <returns>受影响的条数</returns>
        public int UpdateBasicData(BasicDataEntity basicDataEntity)
        {
            LoggerHelper.Info("更新基本数据配置表：" + "id：" + basicDataEntity.ID + "，会议室名称" + basicDataEntity.RoomName + "，最少使用人数" + basicDataEntity.MinUseNumber + ",会议开始前*分钟取钥匙" + basicDataEntity.BeforeTakeKey + "，会议结束前*分钟还钥匙" + basicDataEntity.AfterReturnKey + "，会议室使用时间上限" + basicDataEntity.UpperTime + "，会议室使用时间下限" + basicDataEntity.LowerTime + "，审批人" + basicDataEntity.Approver);

            string sql = "update t_basicdata set room_name=@roomName ,min_use_number=@minUseNumber,before_take_key=@beforeTakeKey,after_return_key=@afterReturnKey,upper_time=@upperTime,lower_time=@lowerTime,approver=@approver,approver_id=@approverID  where id=@id";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@roomName",basicDataEntity.RoomName),
                new MySqlParameter("@minUseNumber",basicDataEntity.MinUseNumber),
                new MySqlParameter("@beforeTakeKey",basicDataEntity.BeforeTakeKey),
                new MySqlParameter("@afterReturnKey",basicDataEntity.AfterReturnKey),
                new MySqlParameter("@upperTime",basicDataEntity.UpperTime),
                new MySqlParameter("@lowerTime",basicDataEntity.LowerTime),
                new MySqlParameter("@approver",basicDataEntity.Approver),
                new MySqlParameter("@approverID",basicDataEntity.ApproverID),
                new MySqlParameter("@id",basicDataEntity.ID)
            };

            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        /// <summary>
        /// 更新t_room表会议室名称
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomName"></param>
        /// <returns>受影响行数</returns>
        public int UpdateRoomName(int id, string roomName)
        {
            LoggerHelper.Info("更新t_room表：" + "id：" + id + "，会议室名称" + roomName);

            string sql = "update t_room set room_name=@roomName where id =@id";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
            {
                new MySqlParameter("@id",id),
                new MySqlParameter("@roomName",roomName)
            };
            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        /// <summary>
        /// t_basicdata表添加教室
        /// </summary>
        /// <param name="basicDataEntity"></param>
        /// <returns>受影响行数</returns>
        public int InsertMetting(BasicDataEntity basicDataEntity)
        {
            LoggerHelper.Info("添加教室：" + "id：" + basicDataEntity.ID + "，会议室名称" + basicDataEntity.RoomName + "，最少使用人数" + basicDataEntity.MinUseNumber + ",会议开始前*分钟取钥匙" + basicDataEntity.BeforeTakeKey + "，会议结束前*分钟还钥匙" + basicDataEntity.AfterReturnKey + "，会议室使用时间上限" + basicDataEntity.UpperTime + "，会议室使用时间下限" + basicDataEntity.LowerTime + "，审批人" + basicDataEntity.Approver);

            string sql = "insert into t_basicdata (id,room_name,min_use_number,before_take_key,after_return_key,upper_time,lower_time,approver)values (@id,@roomName,@minUseNumber,@beforeTakeKey,@afterReturnKey,@upperTime,@lowerTime,@approver)";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
           {
               new MySqlParameter("@id",basicDataEntity.ID),
                new MySqlParameter("@roomName",basicDataEntity.RoomName),
                new MySqlParameter("@minUseNumber",basicDataEntity.MinUseNumber),
                new MySqlParameter("@beforeTakeKey",basicDataEntity.BeforeTakeKey),
                new MySqlParameter("@afterReturnKey",basicDataEntity.AfterReturnKey),
                new MySqlParameter("@upperTime",basicDataEntity.UpperTime),
                new MySqlParameter("@lowerTime",basicDataEntity.LowerTime),
                new MySqlParameter("@approver",basicDataEntity.Approver),

           };

            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }

        /// <summary>
        /// t_room表添加教室
        /// </summary>
        /// <param name="roomEntity"></param>
        /// <returns>受影响行数</returns>
        public int InsertRoomMetting(RoomInfoEntity roomEntity)
        {
            LoggerHelper.Info("添加教室：" + "自增ID：" + roomEntity.ID + "，会议室名称：" + roomEntity.RoomName + "锁编号：" + roomEntity.LockNumber + "，锁的状态：" + roomEntity.LockState);

            string sql = "insert into t_room (id,lock_number,room_name,lock_state) values(@id,@lockNumber,@roomName,@lockState)";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
           {
               new MySqlParameter("@id",roomEntity.ID),
               new MySqlParameter("@lockNumber",roomEntity.LockNumber),
                new MySqlParameter("@roomName",roomEntity.RoomName),
                new MySqlParameter("@lockState",roomEntity.LockState)
           };

            return sqlhelper.ExecuteNonQuery(sql, sqlParameters, CommandType.Text);
        }


        /// <summary>
        /// 获取t_room表已经使用了的锁的编号
        /// </summary>
        /// <returns>List集合</returns>
        public List<string> GetLockNumber()
        {
            //实例化一个List集合用于存储所有已经使用了的锁的编号
            List<string> lockNumberList = new List<string>();

            string sql = "select lock_number from t_room";
            DataTable table = sqlhelper.ExecuteQuery(sql, CommandType.Text);
            foreach (DataRow row in table.Rows)
            {
                lockNumberList.Add(row["lock_number"].ToString());
            }
            return lockNumberList;
        }

        /// <summary>
        /// 删除会议室
        /// </summary>
        /// <param name="roomId">会议室id</param>
        /// <returns>受影响的条数</returns>
        public int DeleteMetting(string roomId)
        {
            LoggerHelper.Info("删除会议室的自增ID：" + "" + roomId);

            string sql1 = "update t_room set is_delete = 1 where id = @id";
            string sql2 = "update t_room set is_delete = 1 where id = @id";
            MySqlParameter[] sqlParameters = new MySqlParameter[]
          {
               new MySqlParameter("@roomId",roomId)

          };

            sqlhelper.ExecuteNonQuery(sql1, sqlParameters, CommandType.Text);
            return sqlhelper.ExecuteNonQuery(sql2, sqlParameters, CommandType.Text);
        }

    }
}
