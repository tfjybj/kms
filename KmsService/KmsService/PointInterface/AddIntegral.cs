/*
 * 创建人：王梦杰
 * 创建时间：2021年12月13日08:47:21
 * 描述：调用积分接口进行扣分
 */

using Newtonsoft.Json;
using KmsService.DingDingModel;
using KmsService.Log4;
using System.Configuration;
namespace KmsService.PointInterface
{
    public class AddIntegral
    {
        /// <summary>
        /// 减分
        /// </summary>
        /// <param name="userID">权限ID</param>
        public string MinusPoints(string userID, string token, string authID)
        {

            //加减分的URL
            string url = ConfigurationManager.ConnectionStrings["pointURL"].ConnectionString;
            PointModel[] pointModel = new PointModel[] { new PointModel() };

            pointModel[0].integral = -5;
            pointModel[0].pluginId = "pluginId_Kms";
            pointModel[0].primaryId = "Kms";
            pointModel[0].reason = "由于您未及时归还钥匙，扣除大米积分5分";
            pointModel[0].typeKey = "typeKey_Kms";
            pointModel[0].userId = authID;

            //定义参数
            //var json_rep = new
            //{
            //    integral = -1,
            //    pluginId = "pluginId_Kms",
            //    primaryId = "Kms",
            //    reason = "由于您未及时归还钥匙，扣除大米积分5分",
            //    typeKey = "typeKey_Kms",
            //    userId = userID
            //};
            //将参数转为string类型
            string jsonData = JsonConvert.SerializeObject(pointModel);
            //执行url
            HttpHelper helper = new HttpHelper();
            string result = helper.HttpPost(url, jsonData, token);
            LoggerHelper.Info("调用积分接口的返回结果：" + result);
            return result;
        }

        /// <summary>
        /// 加分
        /// </summary>
        /// <param name="token">用户对应的token值</param>
        /// <param name="authID">权限ID</param>
        /// <returns></returns>
        public string AddPoints(string token,string authID)
        {
            string url = ConfigurationManager.ConnectionStrings["pointURL"].ConnectionString;
            PointModel[] pointModel = new PointModel[] { new PointModel() };

            pointModel[0].integral =3 ;
            pointModel[0].pluginId = "pluginId_Kms";
            pointModel[0].primaryId = "Kms";
            pointModel[0].reason = "由于您积极主动申请会议室，奖励大米积分3分";
            pointModel[0].typeKey = "typeKey_Kms";
            pointModel[0].userId = authID;

            string jsonData = JsonConvert.SerializeObject(pointModel);
            HttpHelper httpHelper = new HttpHelper();
            string result = httpHelper.HttpPost(url, jsonData, token);
            LoggerHelper.Info("调用加分接口的返回值：" + result);
            return result;
        }
    }
}