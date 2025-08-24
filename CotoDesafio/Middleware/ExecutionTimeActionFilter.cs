// Create in your Infrastructure or Application layer
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace CotoDesafio.Infrastructure.Filters
{
    public class ExecutionTimeActionFilter : IActionFilter
    {
        private Stopwatch _stopwatch;
        private readonly ILogger<ExecutionTimeActionFilter> _logger;

        public ExecutionTimeActionFilter(ILogger<ExecutionTimeActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _stopwatch.Stop();

            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];
            var elapsedMs = _stopwatch.ElapsedMilliseconds;

            _logger.LogInformation(
                "Controller: {Controller}, Action: {Action} executed in {ElapsedMs}ms",
                controllerName, actionName, elapsedMs);
        }
    }
}