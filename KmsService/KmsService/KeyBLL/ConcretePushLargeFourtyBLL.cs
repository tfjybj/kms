/*
 * 创建人：王梦杰
 * 创建时间：2022年1月7日10:23:04
 * 描述：具体处理参会人数大于等于40的类
 */

using System;
using System.Collections.Generic;
using KmsService.DAL;

namespace KmsService.KeyBLL
{
    /// <summary>
    /// 具体处理参会人数大于等于40的类
    /// </summary>
    public class ConcretePushLargeFourtyBLL : PushHandlerBLL
    {
        /// <summary>
        /// 具体处理参会人数大于等于40人的会议室名称
        /// </summary>
        /// <param name="participants">参会人数</param>
        /// <returns>会议室名称</returns>
        public override string HandleRequest(int participants)
        {
            string roomName = null;

            BasicDataDAL selectRoomInfo = new BasicDataDAL();
            List<string> roomPeopleList = selectRoomInfo.SelectMinUseNumber();
            //判断参会人数大于等于40
            if (participants >= Convert.ToInt32(roomPeopleList[2]))
            {
                SelectRoomNameDAL selectRoomName = new SelectRoomNameDAL();
                List<string> list = selectRoomName.SelectRoomNameFourty(roomPeopleList[2]);
                roomName = list[0];
            }
            else if (successor != null)
            {
                //交于下级处理
                roomName = successor.HandleRequest(participants);
            }
            return roomName;
        }
    }
}