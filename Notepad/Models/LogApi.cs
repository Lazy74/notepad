using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Notepad.Models
{
    public class LogApi : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext httpActionContext)
        {
            Database.AddRecordLog(
                "Api",
                httpActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                httpActionContext.ActionDescriptor.ActionName
                );
        }
    }
}