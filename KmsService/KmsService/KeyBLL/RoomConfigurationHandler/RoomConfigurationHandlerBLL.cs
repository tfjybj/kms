/*
 * 创建人：邓礼梅
 * 创建日期：2022年1月11日19:45:39
 * 描述：会议室配置限制
 */
using KmsService.DAL;
using KmsService.Entity;
using System.Collections.Generic;

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

        /// <summary>
        /// 限制判断
        /// </summary>
        /// <param name="basicDataStr">会议室信息字符串</param>
        /// <param name="newBasicData">修改之后的基本数据实体</param>
        /// <param name="oldBasicData">修改前的基本数据实体</param>
        /// <param name="allLockNumber">所有锁号</param>
        /// <returns>bool</returns>
        public abstract bool ModifyRoom(string basicDataStr, BasicDataEntity newBasicData, BasicDataEntity oldBasicData, List<string> allLockNumber);
    }
}