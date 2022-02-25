using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using BeforeEnd.App_Start;
using System;

namespace BeforeEnd
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Ӧ�ó������ʱ���Զ���������log4Net
            log4net.Config.XmlConfigurator.Configure();
            GlobalConfiguration.Configuration.Filters.Add(new Log4.ApiTrackerFilter());

            log4net.Config.XmlConfigurator.Configure();
        }

        //void Application_Error(object sender, System.EventArgs e)
        //{
        //    Exception ex = Server.GetLastError().GetBaseException();
        //    string msg = "\r\n" + "StackTrace:\r\n" + ex.StackTrace
        //                 + "\r\n\r\n" + "Message:\r\n"
        //                 + ex.Message + "\r\n\r\n\r\n\r\n";

        //    //根据不同的Log对象，执行记录
        //    //记录api的异常
        //    log4net.ILog log = log4net.LogManager.GetLogger("APILog");
        //    //记录web的异常
        //    log4net.ILog logg = log4net.LogManager.GetLogger("WebLog");

        //    log4net.Config.XmlConfigurator.Configure();
        //    log.Info(msg);
        //    Server.ClearError();
        //    Response.Redirect("~/ErrorManager/ErrorPage"); 

        //}
    }
}
