using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace GCSEmulator.Filters.Attributes
{
    public class ValidModelStateFilterAttribute : TypeFilterAttribute
    {
        public ValidModelStateFilterAttribute() : base(typeof(ValidModelStateFilterImpl))
        {
        }

        private class ValidModelStateFilterImpl : IActionFilter
        {
            private readonly ILogger<ValidModelStateFilterAttribute> _logger;

            public ValidModelStateFilterImpl(ILoggerFactory loggerFactory)
            {
                _logger = loggerFactory.CreateLogger<ValidModelStateFilterAttribute>();
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (context.ModelState.IsValid)
                    return;

                _logger.LogWarning("Request has invalid model state");

                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }
    }
}