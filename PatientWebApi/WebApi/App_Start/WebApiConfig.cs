using System.Web.Http;
using WebApi.ActionFilter;
using WebApi.ActionFilters;

namespace WebApi
{
    public static class WebApiConfig
    {
        private static object services;

        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ValidateModelStateFilter());
            

            config.Filters.Add(new LoggingFilterAttribute());
            config.Filters.Add(new GlobalExceptionAttribute());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
