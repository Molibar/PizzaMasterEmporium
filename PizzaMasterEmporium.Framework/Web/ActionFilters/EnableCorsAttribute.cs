using System.Collections.Specialized;
using System.Web.Mvc;

namespace PizzaMasterEmporium.Framework.Web.ActionFilters
{
    public class EnableCorsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            var nameValueCollection = new NameValueCollection
                {
                    {"Access-Control-Allow-Origin", "*"},
                    {"Access-Control-Allow-Methods", "GET, POST, DELETE, PUT"},
                    {"Access-Control-Allow-Headers", "Content-Type, Authorization, Accept, CustomAuth"}
                };

            response.Headers.Add(nameValueCollection);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
    }
}