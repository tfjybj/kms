/*
 * 创建人：盖鹏军
 * 时间：2022年2月23日16点54分
 * 描述：配置文件反射
 */
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;


namespace dingdingsuccess.LimitBLL
{
    public class GetConfig
    {
        /// <summary>
        /// 获取配置文件中参数
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigValue(string sectionName, string key)
        {
            //读取配置文件section配置节中的内容。
            NameValueCollection nameValueCollection = (NameValueCollection)ConfigurationManager.GetSection(sectionName);
            //根据key值获取section配置节中的相应value
            return nameValueCollection.Get(key);
        }

        /// <summary>
        /// 配置文件反射生成对象
        /// </summary>
        /// <param name="section">用于读取配置文件section配置节中的内容</param>
        /// <param name="eventType">具体配置</param>
        /// <returns></returns>
        public static dynamic CreateConcreteClass(string section, string eventType)
        {
            //读取配置文件section配置节中的内容。
            NameValueCollection nameValueCollection = (NameValueCollection)ConfigurationManager.GetSection(section);
            //获取具体的要实例化的那个具体类，将该具体类作为参数传给工厂，告诉工厂我要生产这个具体的类。并将创建的具体类返回。           
            return (dynamic)Assembly.GetExecutingAssembly().CreateInstance(nameValueCollection.Get(eventType));
        }

    }
}