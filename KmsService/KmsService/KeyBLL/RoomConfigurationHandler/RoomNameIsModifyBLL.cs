/*
 * 创建人：邓礼梅
 * 创建日期：2022年1月11日19:45:39
 * 描述：会议室配置限制
 */
using KmsService.Entity;
using KmsService.Log4;
using System.Collections.Generic;


namespace KmsService.KeyBLL.RoomConfigurationHandler
{
    //会议室名称是否进行了修改
    class RoomNameIsModifyBLL : RoomConfigurationHandlerBLL
    {
        /// <summary>
        /// 会议室名称是否进行了修改
        /// </summary>
        /// <param name="basicDataStr">会议室信息字符串</param>
        /// <param name="newBasicData">修改之后的基本数据实体</param>
        /// <param name="oldBasicData">修改前的基本数据实体</param>
        /// <param name="allLockNumber">所有锁号</param>
        /// <returns>bool</returns>
        public override bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber)
        {
            LoggerHelper.Info("【管理员会议室配置】会议室名称是否进行了修改：" + "修改之前的会议室名称：" + newBasicData.RoomName + "，修改之后的会议室名称：" + oldBasicData.RoomName);

            //如果会议室名称进行了修改
            if (newBasicData.RoomName != oldBasicData.RoomName)
            {
                LoggerHelper.Info("会议室名称进行了修改，继续走下面的职责链");                
                //调用下一个职责链
                successor.ModifyRoom(basicDataStr, newBasicData, oldBasicData, allLockNumber);
            }
            else
            {
                LoggerHelper.Info("会议室名称没有进行修改，直接更新t_basicData和t_room表");
                //调用B层更新会议室配置信息方法，并传入实体对象参数
                modifyConfigurationDAL.UpdateBasicData(newBasicData);
                modifyConfigurationDAL.UpdateRoomName(newBasicData.ID, newBasicData.RoomName);
            }
            return true;
        }
    }
}