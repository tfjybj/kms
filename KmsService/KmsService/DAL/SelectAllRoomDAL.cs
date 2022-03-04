using System.Collections.Generic;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
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