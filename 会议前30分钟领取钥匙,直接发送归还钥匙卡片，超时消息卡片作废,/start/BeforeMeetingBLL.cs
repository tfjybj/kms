using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using start.ServiceReference;
using start.Log4;
using log4net;
namespace start
{
    internal class BeforeMeetingBLL
    {
        /// <summary>
        /// 会议前30分钟发送卡片消息
        /// </summary>
        public void BeforeMeetingStart()
        {
            HttpHelper httphelper = new HttpHelper();
            ServiceClient service = new ServiceClient();
           
            List<CalendarInfoEntity> TimeAndID = service.BeforeMeetingStart();//时间和钉id;

            DateTime   time ;//计算出的时间
            
            foreach (var item in TimeAndID)
            {
                time = Convert.ToDateTime(item.StartTime).AddMinutes(-30);//计算出会议前30分钟的时间
                string userid = item.OrganizerID;//会议组织人
                string roomName = item.RoomName; //会议室名称
                string ScheduleID = item.CalendarID;//日程id
                if (DateTime.Now>=time)//现在的时间大于计算出来的时间
                {
                    LoggerHelper.Info("查询数据库中的返回值：" + "会议室名称：" + roomName + "日程id" + ScheduleID + "组织者id" + userid);

                    string url = String.Format("http://kms.tfjybj.com/kms/actionapi/SendMessage/SendReceiveCard?roomName={0}&calendarID={1}&userID={2}", roomName, ScheduleID, userid);
                    string OutTrackID = httphelper.HttpGet(url);//接收url的返回值 卡片唯一标识返回值

                    LoggerHelper.Info("调用接口返回结果为：" + OutTrackID);//打印返回结果
                    service.UpdateOutTrackID(ScheduleID, OutTrackID);

                    service.UpdateIsStart(ScheduleID);//给用户发完消息后更改数据库isstart的状态
                }
                
            }
        }


        

    }
}
