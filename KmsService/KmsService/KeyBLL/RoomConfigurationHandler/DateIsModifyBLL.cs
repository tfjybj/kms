using KmsService.Entity;
using KmsService.Log4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KmsService.KeyBLL.RoomConfigurationHandler
{
    //数据是否进行了修改。True:修改了；False：没有修改
    class DateIsModifyBLL : RoomConfigurationHandlerBLL
    {
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