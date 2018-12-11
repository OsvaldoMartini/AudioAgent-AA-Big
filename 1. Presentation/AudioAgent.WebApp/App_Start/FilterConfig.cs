using System.Web.Mvc;
using AudioAgent.WebApp.Filters;

namespace AudioAgent.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

       
    }

}
