/*
 * 创建人：王梦杰
 * 创建日期：2022年3月12日19:45:39
 * 描述：获取审批人信息
 */
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
namespace KmsService.KeyBLL
{
    /// <summary>
    /// 获取审批人
    /// </summary>
    public class GetApproverBLL
    {
        /// <summary>
        /// 获取审批人
        /// </summary>
        /// <returns>管理员姓名集合</returns>
        public List<AllusersEntitiesItem> GetApprover()
        {
            HttpHelper helper = new HttpHelper();
            //url地址
            List<AllusersEntitiesItem> aproverNameList = new List<AllusersEntitiesItem>();
            string userURL = ConfigurationManager.ConnectionStrings["authURL"].ConnectionString;

            string result = helper.HttpGet(userURL);
            //将获取的json串反序列化给实体对象
            Root authManager = JsonConvert.DeserializeObject<Root>(result);
            if (authManager.data[1].roleName == "审批人")
            {
                aproverNameList.AddRange(authManager.data[1].allusersEntities);
            }
            return aproverNameList;
        }
    }
}