/*
 * 创建人：邓礼梅
 * 创建日期：2022年1月11日19:45:39
 * 描述：会议室配置限制
 */
using KmsService.Entity;
using KmsService.Log4;
using System;
using System.Collections.Generic;

namespace KmsService.KeyBLL.RoomConfigurationHandler
{
    //判断此会议室名称是否已存在 ,如果为True，表示存在此会议室名称
    class RoomNameIsExistsBLL : RoomConfigurationHandlerBLL
    {
        public override bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber)
        {
            //调用D层查询数据库是否存在此会议室名称是否已存在。True：存在
            bool flag = modifyConfigurationDAL.RoomNameIsExists(newBasicData.RoomName);
            LoggerHelper.Info("【管理员会议室配置】判断此会议室名称是否已存在：" + flag);

            if (flag)
            {
                //抛出错误提示
                throw new Exception("已存在此名称的会议室");
            }
            else
            {
                //调用下一个职责链
                successor.ModifyRoom(basicDataStr, newBasicData, oldBasicData, allLockNumber);
            }
            return true;
        }
    }
}