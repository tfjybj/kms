using System.Web;
using System.Web.Mvc;

namespace dingdingsuccess
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           
            //API日志
            filters.Add(new Log4.ApiTrackerFilter());
            //监控日志
            filters.Add(new Log4.TrackerFilter());

            filters.Add(new HandleErrorAttribute());

        }
    }
}
