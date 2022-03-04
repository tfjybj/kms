using System.Collections.Generic;
using KmsService.DAL;
using KmsService.DingDingInterface;

namespace KmsService.KeyBLL
{
    /*

     功能：给用户发消息的方法，
     */

    public class SendMessages
    {
        public void SendMessageUser()
        {
            PersonReportDAL prd = new PersonReportDAL();
            PushWeeklyReport pwr = new PushWeeklyReport();
            List<string> dd = prd.AllddID();//获取查到的钉id

            foreach (var item in dd)
            {
                pwr.message(item.ToString());//调用message接口，把获取的id传入
            }
        }
    }
}