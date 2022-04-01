/*
 * 创建人：邓礼梅
 * 创建日期：2022年1月11日19:45:39
 * 描述：修改教室
 */
using KmsService.Entity;
using System.Collections.Generic;

namespace KmsService.KeyBLL.RoomConfigurationHandler
{
    /// <summary>
    /// 修改会议室信息
    /// </summary>
    public class ModifyRoomBLL
    {
        /// <summary>
        /// 设置上下级
        /// </summary>
        /// <param name="basicDataStr">要修改信息的字符串</param>
        /// <param name="newBasicData">修改之后的信息</param>
        /// <param name="oldBasicData">修改之前的信息</param>
        /// <returns>bool</returns>
        public bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber)
        {
            RoomConfigurationHandlerBLL judge = new JudgeBLL();
            RoomConfigurationHandlerBLL range = new RangeBLL();
            RoomConfigurationHandlerBLL dateIsModify = new DateIsModifyBLL();
            RoomConfigurationHandlerBLL roomNameIsModify = new RoomNameIsModifyBLL();
            RoomConfigurationHandlerBLL roomNameIsExists = new RoomNameIsExistsBLL();
            RoomConfigurationHandlerBLL roomIdIsExists = new RoomIdIsExistsBLL();
            RoomConfigurationHandlerBLL insertRoom = new InsertRoomBLL();

            judge.SetSuccessor(range);
            range.SetSuccessor(dateIsModify);
            dateIsModify.SetSuccessor(roomNameIsModify);
            roomNameIsModify.SetSuccessor(roomNameIsExists);
            roomNameIsExists.SetSuccessor(roomIdIsExists);
            roomIdIsExists.SetSuccessor(insertRoom);

            return judge.ModifyRoom(basicDataStr, newBasicData, oldBasicData, allLockNumber);
        }
    }
}