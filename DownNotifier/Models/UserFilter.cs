using DownNotifier.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DownNotifier
{
    /// <summary>
    /// It is used to prevent the user who is not sign in from being able to take action.
    /// </summary>
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string userid = context.HttpContext.Session.Get<string>(SessionKeys.UserId);

            if (userid == null || userid == "")
            {
                context.Result = new RedirectResult("/Sign/SignIn");
            }
        }
    }
}
