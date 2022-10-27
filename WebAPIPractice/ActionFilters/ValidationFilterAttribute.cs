using Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AWebAPIPractice.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        private readonly ILoggerManager _loggerManager;

        public ValidationFilterAttribute(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            //int id = (int)context.ActionArguments["companyId"];

            var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if(param == null)
            {
                _loggerManager.LogError($"Object sent from client is null COntroller: {controller} and action: {action}");
                context.Result = new BadRequestObjectResult($"Object sent from client is null COntroller: {controller} and action: {action}");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                _loggerManager.LogError($"Object sent from client is null COntroller: {controller} and action: {action}");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
