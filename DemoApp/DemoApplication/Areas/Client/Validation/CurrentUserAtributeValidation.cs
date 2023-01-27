using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoApplication.Areas.Client.Validation
{
    public class CurrentUserAtributeValidation :IActionFilter
    {
        //private readonly IUserService _userService;

        //public ValidationCurrentUserAttribute(IUserService userService)
        //{
        //    _userService = userService;
        //}
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var user = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            //if (!user.IsAuthenticated)
            //{
            //    var controller = (AuthenticationController)context.Controller;
            //    context.Result = controller.RedirectToRoute("client-login");
            //}
        }
    }
}
