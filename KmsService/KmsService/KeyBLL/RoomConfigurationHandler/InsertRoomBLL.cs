/*
 * 创建人：邓礼梅
 * 创建日期：2022年1月11日19:45:39
 * 描述：插入新教室
 */
using KmsService.Entity;
using System;
using System.Collections.Generic;
using KmsService.Log4;

namespace KmsService.KeyBLL.RoomConfigurationHandler
{
    //插入新教室
    class InsertRoomBLL : RoomConfigurationHandlerBLL
    {

        /// <summary>
        /// 插入新教室
        /// </summary>
        /// <param name="basicDataStr">会议室信息字符串</param>
        /// <param name="newBasicData">修改之后的基本数据实体</param>
        /// <param name="oldBasicData">修改前的基本数据实体</param>
        /// <param name="allLockNumber">所有锁号</param>
        /// <returns>bool</returns>
        public override bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber)
        {
            //获取要添加的会议室信息
            roomInfo.ID = newBasicData.ID;
            roomInfo.LockNumber = allLockNumber[0];
            roomInfo.RoomName = newBasicData.RoomName;
            roomInfo.LockState = roomInfo.LockNumber + "isLock";

            LoggerHelper.Info("【管理员会议室配置】不存在id为" + Convert.ToString(newBasicData.ID) + "的会议室，说明管理员要添加会议室，往t_basicdata表和t_room表插入信息为：会议室id：" + roomInfo.ID + "，锁号：" + roomInfo.LockNumber + "，会议室名称" + roomInfo.RoomName + "，会议室状态：" + roomInfo.LockState + "，最少使用人数：" + newBasicData.MinUseNumber + "，会议开始前*分钟取钥匙：" + newBasicData.BeforeTakeKey + ",会议室结束前*分钟还钥匙：" + newBasicData.AfterReturnKey + ",时间上限:" + newBasicData.UpperTime + "，时间下限：" + newBasicData.LowerTime);

            //t_basicdata表插入会议室信息
            modifyConfigurationDAL.InsertMetting(newBasicData);
            //t_room表插入会议室信息
            modifyConfigurationDAL.InsertRoomMetting(roomInfo);
            btnState = false;

            LoggerHelper.Info("往数据库插入会议室信息没问题，添加会议室成功，添加会议室按钮状态为false");
            return btnState;
        }
    }
}