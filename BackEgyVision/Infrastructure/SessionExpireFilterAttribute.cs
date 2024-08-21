using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication;
using System;

namespace BackEgyVision.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If the browser session or authentication session has expired...
            if (filterContext.HttpContext.Session.GetString("TestSession") == null || !filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    // For AJAX requests, we're overriding the returned JSON result with a simple string,
                    // indicating to the calling JavaScript code that a redirect should be performed.
                    filterContext.Result = new JsonResult("{ Data = \"_Logon_\" }");
                }
                else
                {
                    // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                    // simply displays a temporary 5 second notification that they have timed out, and
                    // will, in turn, redirect to the logon page.

                    AuthenticationHttpContextExtensions.SignOutAsync(filterContext.HttpContext);
                    filterContext.HttpContext.Session.Clear();

                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Controller", "Account" },
                        { "Action", "Logout" }
                        //{ "Action", "TimeoutRedirect" }
                    });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class SessionExpireAdminFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // If the browser session or authentication session has expired... TestSession
            if ((filterContext.HttpContext.Session.GetString("TestSession") == null) && (!filterContext.HttpContext.Request.Path.ToString().ToLower().Contains("/admin/verifycodemobile")) && (!filterContext.HttpContext.Request.Path.ToString().ToLower().Contains("/admin/manage")) && (!filterContext.HttpContext.Request.Path.ToString().ToLower().Contains("/admin/createuser")))
            {
                if (filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    // For AJAX requests, we're overriding the returned JSON result with a simple string,
                    // indicating to the calling JavaScript code that a redirect should be performed.
                    filterContext.Result = new JsonResult("{ Data = \"_Logon_\" }");
                }
                else
                {
                    // For round-trip posts, we're forcing a redirect to Home/TimeoutRedirect/, which
                    // simply displays a temporary 5 second notification that they have timed out, and
                    // will, in turn, redirect to the logon page.

                   // filterContext.HttpContext.Authentication.SignOutAsync("EgyVision-Back-auth");

                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                        { "Controller", "Account" },
                        { "Action", "Logout" }
                        //{ "Action", "TimeoutRedirect" }
                    });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
