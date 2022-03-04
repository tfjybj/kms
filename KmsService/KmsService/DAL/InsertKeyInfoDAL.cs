using MySql.Data.MySqlClient;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
    public class InsertKeyInfoDAL
    {
        public int InsertKeyInfo(RoomInfoEntity roomInfo)
        {
            string sql = "insert into t_room set room_name=@RoomName,lock_number=@LockNumber,lock_state=@LockState,front_min=@FrontMin,min_use_number=@MinUseNumber,create_time=@CreateTime,update_time=@UpdateTime";
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter("@RoomName",roomInfo.RoomName),
                new MySqlParameter("@LockNumber",roomInfo.LockNumber),
                new MySqlParameter("@LockState",roomInfo.LockState),
                new MySqlParameter("@FrontMin",roomInfo.FrontMin),
                new MySqlParameter("@MinUseNumber",roomInfo.MinUseNumber),
                new MySqlParameter("@CreateTime",roomInfo.CreateTime),
                new MySqlParameter("@UpdateTime",roomInfo.UpdateTime)
            };

            SQLHelper helper = new SQLHelper();
            return helper.ExecuteNonQuery(sql, mySqlParameter, CommandType.Text);
        }
    }
}