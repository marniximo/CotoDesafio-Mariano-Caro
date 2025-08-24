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

        /// <summary>
        /// Logs the execution time of an action after it has been executed.
        /// </summary>
        /// <remarks>This method stops the internal stopwatch used to measure the elapsed time of the
        /// action's execution  and logs the controller name, action name, and execution duration in
        /// milliseconds.</remarks>
        /// <param name="context">The <see cref="ActionExecutedContext"/> containing information about the executed action,  such as route
        /// data and the action's execution context.</param>
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