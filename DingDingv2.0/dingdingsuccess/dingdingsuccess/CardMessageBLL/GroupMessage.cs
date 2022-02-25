
using dingdingsuccess.KMSDAL;

namespace dingdingsuccess.CardMessageBLL
{
    //未使用
    /// <summary>
    /// 通过钉钉群发送消息
    /// </summary>
    public class GroupMessage
    {
        /// <summary>
        /// 根据用户ID发送消息链接
        /// </summary>
        /// <param name="userid">钉ID</param>
        /// <param name="CalendarEventId">日程ID</param>
        public void SendMessage(string userid,string CalendarEventId)
        {
            UserGroupDAL userGroupDAL = new UserGroupDAL();
            string chatid = userGroupDAL.SelectChatID(userid);
            //判断数据库中是否查到对应用户的群ID，如果有则直接通过群ID发送消息
            if (chatid!= "flase")
            {
                new SendDingGroupMessage().SendMessage(chatid,CalendarEventId,userid);
            }
            else//如果没有先创建群，然后把群ID以及用户ID存入数据库，然后发送消息
            {
                //创建群
                chatid=new CreateDingGroup().CreateMessageGroup(userid);
                //ID存入数据库
                userGroupDAL.Insert(userid, chatid);
                //发送消息
                new SendDingGroupMessage().SendMessage(chatid, CalendarEventId,userid);
            }
            
        }
    }
}