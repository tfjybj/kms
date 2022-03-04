 /*
 * 创建人：武梓龙
 * 创建时间：2022年1月7日09点33分
 * 描述：未正常归还钥匙进行扣分处理
 */

using System;
using System.Collections.Generic;
using KmsService.AuthInterface;
using KmsService.DAL;
using KmsService.DingDingInterface;
using KmsService.DingDingModel;
using KmsService.Entity;
using KmsService.Log4;
using KmsService.PointInterface;

namespace KmsService.KeyBLL
{
    public class ReturnKeyLateBLL
    {
        /// <summary>
        /// 判断钥匙归还时间
        /// </summary>
        /// <param name="calendarID">日程id</param>
        public string ReturnKeyLate(string calendarID, string userid)
        {
            string result = null;
            string organizerID = null;
            string superAdminToken = null;
            string userAuthID = null;
            try
            {
                //调用钉钉获取手机号的接口
                GetUnionID getUnion = new GetUnionID();
                GetUnionIDModel userModel = getUnion.GetDingDingUnionID(userid);  
                string phoneNumber = userModel.result.mobile;

                GetUserToken userToken = new GetUserToken();
                UserTokenModel token = userToken.GetToken("superAdmin");

                UserTokenModel authID = userToken.GetToken(phoneNumber);

                SelectCalendarInfoDAL SelectAttendPerson = new SelectCalendarInfoDAL();
                CalendarInfoEntity calendardata = SelectAttendPerson.SelectCalendarInfo(calendarID);

                organizerID = calendardata.OrganizerID;
                superAdminToken = token.data.token;
                userAuthID = authID.data.userId;
                if (DateTime.Compare(Convert.ToDateTime(calendardata.ReturnTime), Convert.ToDateTime(calendardata.EndTime)) > 0)
                {
                    AddIntegral addIntegral = new AddIntegral();
                    result = addIntegral.MinusPoints(organizerID, superAdminToken, userAuthID);
                    LoggerHelper.Info("\n调用MinusPoints方法的参数：OrganizerID、token、userId" + organizerID + superAdminToken + userAuthID);
                }
                return result;
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用超时归还钥匙方法错误信息：" + ex.Message + "\n堆栈信息：" + ex.StackTrace + "\n报异常的方法名称：" + ex.TargetSite+ "\n调用MinusPoints方法的参数：OrganizerID、token、userId" + organizerID +"、"+ superAdminToken +"、"+ userAuthID);
                return result;
            }
        }
    }
}