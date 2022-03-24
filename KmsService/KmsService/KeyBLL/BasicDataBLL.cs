/*
 * 创建人：王梦杰
 * 创建时间：2022年1月11日19:49:46
 * 描述：基本数据配置表逻辑层
 */

using System;
using KmsService.DAL;
using KmsService.Entity;
using KmsService.Log4;

namespace KmsService.KeyBLL
{
    /// <summary>
    /// 基本数据配置表B层类
    /// </summary>
    public class BasicDataBLL
    {
        /// <summary>
        /// 查询基本数据配置表全部信息
        /// </summary>
        /// <returns></returns>
        public BasicDataEntity SelectALLBasicData(string roomName)
        {
            BasicDataEntity basicDataEntity = new BasicDataEntity();
            try
            {
                BasicDataDAL basicData = new BasicDataDAL();
                basicDataEntity = basicData.SelectAllBasicData(roomName);
            }
            catch (Exception ex)
            {
                LoggerHelper.Error("调用查询基本数据配置表全部信息方法的错误信息：" + ex.Message + "堆栈信息：" + ex.StackTrace);

            }
            return basicDataEntity;
        }
    }
}