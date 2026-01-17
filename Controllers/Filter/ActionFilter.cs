using Microsoft.AspNetCore.Mvc.Filters;
using SimpleECommerce.InfraStructure.Logging;

namespace SimpleECommerce.Controllers.Filter
{
    public class ActionFilter : IActionFilter
    {
        private readonly IAppLogger<ActionFilter> _logger;

        public ActionFilter(IAppLogger<ActionFilter> logger)
        { 
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                _logger.Error(
                    context.Exception,
                    $"{context.ActionDescriptor.DisplayName} : Exception",
                    new
                    {
                        TraceId = context.HttpContext.TraceIdentifier,
                        User = context.HttpContext.User?.Identity?.Name,
                        Controller = context.Controller.GetType().Name,
                        Canceled = context.Canceled,
                        Route = context.RouteData.Values
                    }
                );
            }
            else
            {
                _logger.Info(
                    $"{context.ActionDescriptor.DisplayName} : End",
                    new
                    {
                        TraceId = context.HttpContext.TraceIdentifier,
                        User = context.HttpContext.User?.Identity?.Name,
                        Controller = context.Controller.GetType().Name,
                        Canceled = context.Canceled,
                        Route = context.RouteData.Values.ToDictionary(
                            entry => entry.Key,
                            entry => entry.Value?.ToString()
                        )
                    }
                );
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.Info(
                $"{context.ActionDescriptor.DisplayName} : Start",
                new
                {
                    TraceId = context.HttpContext.TraceIdentifier,
                    User = context.HttpContext.User?.Identity?.Name,
                    Controller = context.Controller.GetType().Name,
                    Args = context.ActionArguments.ToDictionary(
                        entry => entry.Key,
                        entry => entry.Value?.ToString()
                    ),
                    Route = context.RouteData.Values.ToDictionary(
                        entry => entry.Key,
                        entry => entry.Value?.ToString()
                    )
                }
            );
        }
    }
}
