/*
 * 创建人：王梦杰
 * 创建日期：2022年1月11日19:45:39
 * 描述：查询所有会议室D层操作类
 */
using System.Collections.Generic;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
    /// <summary>
    /// 查询所有会议室类
    /// </summary>
    public class SelectAllRoomDAL
    {
        /// <summary>
        /// 查询会议室表中所有可用会议室
        /// </summary>
        /// <returns>可用会议室集合</returns>
        public List<RoomInfoEntity> SelectAllRoom()
        {
            string sql = "select room_name,lock_number from t_room where lock_state LIKE '%isLock'";

            SQLHelper helper = new SQLHelper();
            DataTable dataTable = helper.ExecuteQuery(sql, CommandType.Text);
            List<RoomInfoEntity> roomInfo = new List<RoomInfoEntity>();
            foreach (DataRow row in dataTable.Rows)
            {
                roomInfo.Add(new RoomInfoEntity
                {
                    RoomName = row["room_name"].ToString(),
                    LockNumber = row["lock_number"].ToString()
                });
            }

            return roomInfo;
        }
    }
}