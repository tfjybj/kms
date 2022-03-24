/*
 * 创建人：王梦杰
 * 创建时间：2022年1月7日10:06:52
 * 描述：具体处理参会人数在十到二十之间的类
 */

using System;
using System.Collections.Generic;
using KmsService.DAL;

namespace KmsService.KeyBLL
{
    /// <summary>
    /// 具体处理参会人数在会议室最低使用人数和会议室使用上限人数之间
    /// </summary>
    public class ConcretePushTwentyRoomBLL : PushHandlerBLL
    {
        /// <summary>
        /// 具体处理参会人数在会议室最低使用人数和会议室使用上限人数之间
        /// </summary>
        /// <param name="participants">参会人数</param>
        /// <returns>会议室名称</returns>
        public override string HandleRequest(int participants)
        {
            string roomName = null;

            BasicDataDAL selectRoomInfo = new BasicDataDAL();
            List<string> roomPeopleList = selectRoomInfo.SelectMinUseNumber();
            //判断参会人数在会议室最低使用人数和会议室使用上限人数之间
            if (participants >= Convert.ToInt32(roomPeopleList[Invariable.Zero]) && participants < Convert.ToInt32(roomPeopleList[Invariable.One]))
            {
                SelectRoomNameDAL selectRoomName = new SelectRoomNameDAL();
                List<string> list = selectRoomName.SelectRoomNameTen(roomPeopleList[Invariable.Zero], roomPeopleList[Invariable.One]);
                roomName = list[Invariable.Zero];
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