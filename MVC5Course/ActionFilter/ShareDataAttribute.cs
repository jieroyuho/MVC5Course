using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class ShareDataAttribute : ActionFilterAttribute //要選 System.Web.Mvc; 而不是using System.Web.Http.Filters;
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext) 
        {
            filterContext.Controller.ViewData["Temp1"] = "暫存資料 Temp1";
        }
    }

}