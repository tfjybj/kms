using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class AllusersEntitiesItem
{
    /// <summary>
    /// 
    /// </summary>
    public string isSelect { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string dingId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string userCode { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string password { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string createTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string phone { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tenantId { get; set; }
}

public class DataItem
{
    /// <summary>
    /// 
    /// </summary>
    public string roleId { get; set; }
    /// <summary>
    /// 管理员功能
    /// </summary>
    public string roleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<AllusersEntitiesItem> allusersEntities { get; set; }
}

public class Root
{
    /// <summary>
    /// 
    /// </summary>
    public string code { get; set; }
    /// <summary>
    /// 该项目下的角色查询成功
    /// </summary>
    public string message { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<DataItem> data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int count { get; set; }
}
