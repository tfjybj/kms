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
    //时间范围限制
    class RangeBLL : RoomConfigurationHandlerBLL
    {
        /// <summary>
        /// 限制判断
        /// </summary>
        /// <param name="basicDataStr">会议室信息字符串</param>
        /// <param name="newBasicData">修改之后的基本数据实体</param>
        /// <param name="oldBasicData">修改前的基本数据实体</param>
        /// <param name="allLockNumber">所有锁号</param>
        /// <returns>bool</returns>
        public override bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber)
        {
            LoggerHelper.Info("【管理员会议室配置】时间范围限制：" + "要限制的字符串：" + "时间上限：" + newBasicData.UpperTime + ",时间下限" + newBasicData.LowerTime + ",会议结束前*分钟还钥匙" + newBasicData.AfterReturnKey);

            //时间上限、时间下限、会议结束前*分钟还钥匙逻辑判断
            if (!(newBasicData.UpperTime > newBasicData.LowerTime))
            {
                throw new Exception("时间上限不能小于时间下限");
            }
            else if (!(newBasicData.AfterReturnKey <= newBasicData.LowerTime))
            {
                throw new Exception("时间下限不能小于会议结束前*分钟还钥匙");
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