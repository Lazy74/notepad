using System.Web.Mvc;

namespace Notepad.Models
{
    public class ActLog : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext executedContext)
        {
            Database.AddRecordLog(
                "MVC",
                executedContext.RouteData.GetRequiredString("controller"),
                executedContext.RouteData.GetRequiredString("action")
                );
        }
    }
}