using Microsoft.AspNetCore.Mvc.Filters;


namespace Hyper.Api.ActionFilters
{
    public class LowercaseNaturalKeysAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // SQL is case sensitive but HTTP is not
            // All natural keys are enforced lowercase
            if (filterContext.ActionArguments.ContainsKey("Id")) filterContext.ActionArguments["Id"] = filterContext.ActionArguments["Id"].ToString().ToLower();

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}