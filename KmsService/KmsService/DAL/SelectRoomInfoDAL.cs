/*
 * 创建人：王梦杰
 * 创建时间：2021年12月21日11:44:13
 * 描述：查询钥匙对应的锁号
 */

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using KmsService.Entity;

namespace KmsService.DAL
{
    public class SelectRoomInfoDAL
    {
        public RoomInfoEntity SelectRoomInfo(string roomName)
        {
            string sql = "select id,lock_number,room_name,lock_state,front_min,min_use_number,create_time,update_time from t_room where room_name=@roomName";
            MySqlParameter[] mySqlParameter = new MySqlParameter[]
            {
                new MySqlParameter("@roomName", roomName)
            };
            SQLHelper helper = new SQLHelper();
            DataTable dataTable = helper.ExecuteQuery(sql, mySqlParameter, CommandType.Text);
            RoomInfoEntity roomInfo = new RoomInfoEntity();
            foreach (DataRow row in dataTable.Rows)
            {
                roomInfo.ID = Convert.ToInt32(row["id"]);
                roomInfo.LockNumber = row["lock_number"].ToString();
                roomInfo.RoomName = row["room_name"].ToString();
                roomInfo.LockState = row["lock_state"].ToString();
                roomInfo.FrontMin = Convert.ToInt32(row["front_min"]);
                roomInfo.MinUseNumber = Convert.ToInt32(row["min_use_number"]);
                roomInfo.CreateTime = row["create_time"].ToString();
                roomInfo.UpdateTime = row["update_time"].ToString();
            }
            //string lockNumber = t_Room_Info.LockNumber.ToString();
            return roomInfo;
        }

        /// <summary>
        /// 查询所有房间的最少使用人数
        /// </summary>
        /// <returns>所有房间的最少使用人数集合</returns>
        public List<string> SelectRoomPeople()
        {
            string sql = "select DISTINCT min_use_number from t_room where min_use_number is not NULL";
            SQLHelper helper = new SQLHelper();
            DataTable roomPeopleTable = helper.ExecuteQuery(sql, CommandType.Text);
            List<string> roomPeopleList = new List<string>();
            foreach (DataRow row in roomPeopleTable.Rows)
            {
                roomPeopleList.Add(row["min_use_number"].ToString());
            }

            return roomPeopleList;
        }

        /// <summary>
        /// 根据房间名称查询未被申请使用的房间
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public RoomInfoEntity SelectRoomName(string roomName)
        {
            string sql = "select id,lock_number,room_name,lock_state,front_min,min_use_number,create_time,update_time from t_room where room_name=@roomName and lock_state like '%isLock%'";
            MySqlParameter[] sqlParameters = new MySqlParameter[] { new MySqlParameter("@roomName", roomName) };
            SQLHelper helper = new SQLHelper();
            DataTable roomNameTable = helper.ExecuteQuery(sql, sqlParameters, CommandType.Text);
            RoomInfoEntity roomInfo = new RoomInfoEntity();
            foreach (DataRow row in roomNameTable.Rows)
            {
                roomInfo.ID = Convert.ToInt32(row["id"]);
                roomInfo.LockNumber = row["lock_number"].ToString();
                roomInfo.RoomName = row["room_name"].ToString();
                roomInfo.LockState = row["lock_state"].ToString();
                roomInfo.FrontMin = Convert.ToInt32(row["front_min"]);
                roomInfo.MinUseNumber = Convert.ToInt32(row["min_use_number"]);
                roomInfo.CreateTime = row["create_time"].ToString();
                roomInfo.UpdateTime = row["update_time"].ToString();
            }
            return roomInfo;
        }
    }
}