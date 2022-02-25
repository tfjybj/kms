using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeforeEnd.App_Start
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
