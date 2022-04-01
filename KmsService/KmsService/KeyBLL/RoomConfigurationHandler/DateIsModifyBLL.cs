/*
 * 创建人：邓礼梅
 * 创建日期：2022年1月11日19:45:39
 * 描述：数据是否进行了修改。True:修改了；False：没有修改
 */
using KmsService.Entity;
using KmsService.Log4;
using System.Collections.Generic;

namespace KmsService.KeyBLL.RoomConfigurationHandler
{
    //数据是否进行了修改。True:修改了；False：没有修改
     class DateIsModifyBLL : RoomConfigurationHandlerBLL
    {
        /// <summary>
        /// 修改会议室配置
        /// </summary>
        /// <param name="basicDataStr">会议室信息字符串</param>
        /// <param name="newBasicData">修改之后的基本数据实体</param>
        /// <param name="oldBasicData">修改前的基本数据实体</param>
        /// <param name="allLockNumber">所有锁号</param>
        /// <returns>bool</returns>
        public override bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber)
        {
            if (!(oldBasicData.RoomName == newBasicData.RoomName && oldBasicData.MinUseNumber == newBasicData.MinUseNumber && oldBasicData.BeforeTakeKey == newBasicData.BeforeTakeKey && oldBasicData.AfterReturnKey == newBasicData.AfterReturnKey && oldBasicData.UpperTime == newBasicData.UpperTime && oldBasicData.LowerTime == newBasicData.LowerTime && oldBasicData.Approver == newBasicData.Approver))
            {
                LoggerHelper.Info("管理员修改了会议室信息，继续走下面的职责链");
                //调用下一个职责链
                successor.ModifyRoom(basicDataStr, newBasicData, oldBasicData, allLockNumber);
            }
            return true;
        }
    }
}