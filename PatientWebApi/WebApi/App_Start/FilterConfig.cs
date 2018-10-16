using System.Web.Mvc;
using WebApi.ActionFilter;

namespace WebApi
{
    public class FilterConfig
    {
        private static object services;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
     
        }
    }
}