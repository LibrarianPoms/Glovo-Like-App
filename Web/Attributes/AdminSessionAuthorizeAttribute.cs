using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aymane.netProjet.Attributes
{
    public class AdminSessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                context.Result = new RedirectToActionResult("Login", "Client", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
