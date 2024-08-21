using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FileResult = Microsoft.AspNetCore.Mvc.FileResult;

namespace EgyVisionService.HelperServices
{
    public class NotFoundFileResult : FileResult
    {
        public NotFoundFileResult(string contentType) : base(contentType)
        {

        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            return base.ExecuteResultAsync(context);
        }

        public override void ExecuteResult(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            context.HttpContext.Response.StatusCode = 404;
        }
    }
}
