using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using start.ServiceReference;

namespace start
{
    internal class CancelCard
    {
        public void Cancel()
        {
            HttpHelper httphelper = new HttpHelper();
            ServiceClient service = new ServiceClient();

            
            List<CalendarInfoEntity> information = service.CancelCard();
            string newtrackid = null;//存放分割之后的消息卡片id
            string roomName = null;//存放开始时间>=会议开始时间的教室名称
            

            foreach (var item in information)
            {
                //切割消息卡片id的字符串
                string trackid = item.OutTrackID;//卡片消息id
                char[] chr = new char[] { '"' };
                string[] result = trackid.Split(chr, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < result.Length; i++)
                {
                    newtrackid = result[i].ToString();
                }
                
                string calendar= item.CalendarID;//日程id
                //当前时间>=会议开始时间
                if (DateTime.Now > Convert.ToDateTime(item.StartTime).AddMinutes (5))
                {
                   
                    roomName = item.RoomName;//查出符合条件的教室

                    RoomInfoEntity RoomInformation = service.SelectRoomInfo(roomName);//查出教室的锁号
                   string lockstate= RoomInformation.LockNumber + "isLock";//锁号+教室名称
                    //当查询到的教室的状态是否是开
                    string state = service.RoomState(roomName);
                    //string newState = state.Substring(2, 6);

                    if (state.Contains("used"))//字符串中不包含isopen
                    {
                        service.UpdateLockState(roomName, lockstate);//修改教室的状态

                        string url = string.Format("http://kms.tfjybj.com/kms/actionapi/SendMessage/UpdateReceiveCard?roomName={0}&OutTrackId={1}", roomName, newtrackid);
                        service.UpdateIsEnd(calendar);//更新isend状态
                        httphelper.HttpGet(url);
                    }
                   
                }

            }

        }
    }
}
