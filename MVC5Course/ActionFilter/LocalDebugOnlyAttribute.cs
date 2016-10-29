using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class LocalDebugOnlyAttribute : ActionFilterAttribute  //要選 System.Web.Mvc; 而不是using System.Web.Http.Filters;
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }

    }

}