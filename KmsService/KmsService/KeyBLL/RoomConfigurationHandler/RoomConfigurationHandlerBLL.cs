using KmsService.DAL;
using KmsService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KmsService.KeyBLL.RoomConfigurationHandler
{
    abstract class RoomConfigurationHandlerBLL
    {
        protected RoomConfigurationHandlerBLL successor;
        protected ModifyConfigurationDAL modifyConfigurationDAL = new ModifyConfigurationDAL();
        protected RoomInfoEntity roomInfo = new RoomInfoEntity();
        protected List<string> lockNumbers = new List<string>();     //已经使用了的锁号
        protected List<string> allLockNumber = new List<string>();   //所有的锁号
        protected static bool btnState = false;    //静态变量，用于记录添加会议室按钮点击状态

        //设置上下级
        public void SetSuccessor(RoomConfigurationHandlerBLL successor)
        {
            this.successor = successor;
        }

        public abstract bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber);
    }
}