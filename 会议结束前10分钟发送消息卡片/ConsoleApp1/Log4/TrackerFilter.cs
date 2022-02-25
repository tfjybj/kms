using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeforeEnd.Log4
{
    /*******************************************
    Version:
    Creator: 邓礼梅
    CreateDate:2022/2/5 15:17:42
    Function:
    Component:
    *******************************************/

    /// <summary>
    /// 跟踪过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class TrackerFilter : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter, System.Web.Mvc.IResultFilter, System.Web.Mvc.IExceptionFilter
    {
        private readonly string key = "_thisOnActionMonitorLog_";

        #region Action时间监控
        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            MonitorLog monLog = new MonitorLog();
            monLog.ExecuteStartTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff", DateTimeFormatInfo.InvariantInfo));
            monLog.ControllerName = filterContext.RouteData.Values["controller"] as string;
            monLog.ActionName = filterContext.RouteData.Values["action"] as string;
            filterContext.Controller.ViewData[this.key] = monLog;
        }

        /// <summary>
        /// OnActionExecuted
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            MonitorLog monLog = filterContext.Controller.ViewData[this.key] as MonitorLog;
            monLog.ExecuteEndTime = DateTime.Now;
            monLog.FormCollections = filterContext.HttpContext.Request.Form;//form表单提交的数据
            monLog.QueryCollections = filterContext.HttpContext.Request.QueryString;//Url 参数
            LoggerHelper.Monitor(monLog.GetLogInfo());
        }
        #endregion

        #region View 视图生成时间监控
        /// <summary>
        /// OnResultExecuting
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(System.Web.Mvc.ResultExecutingContext filterContext)
        {
            MonitorLog monLog = filterContext.Controller.ViewData[this.key] as MonitorLog;
            monLog.ExecuteStartTime = DateTime.Now;
        }

        /// <summary>
        /// OnResultExecuted
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(System.Web.Mvc.ResultExecutedContext filterContext)
        {
            MonitorLog monLog = filterContext.Controller.ViewData[this.key] as MonitorLog;
            monLog.ExecuteEndTime = DateTime.Now;
            LoggerHelper.Monitor(monLog.GetLogInfo(MonitorLog.MonitorType.View));
            filterContext.Controller.ViewData.Remove(this.key);
        }
        #endregion

        #region 错误日志
        /// <summary>
        /// OnException
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string controllerName = string.Format("{0}Controller", filterContext.RouteData.Values["controller"] as string);
                string actionName = filterContext.RouteData.Values["action"] as string;
                string errorMsg = string.Format("在执行 controller[{0}] 的 action[{1}] 时产生异常", controllerName, actionName);
                LoggerHelper.Error(errorMsg, filterContext.Exception);
            }
        }

        /// <summary>
        /// OnAuthorization
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //这个方法是在Action执行之前调用
            //var user = filterContext.HttpContext.Session["userName"];
            //if (user == null)
            //{
            //    //filterConetext.HttpContext.Response.Redirect("/Login/index");
            //    var url = new UrlHelper(filterContext.RequestContext);
            //    var urls = url.Action("Index", "Login");
            //    filterContext.Result = new RedirectResult(urls);
            //}
        }
        #endregion
    }
}