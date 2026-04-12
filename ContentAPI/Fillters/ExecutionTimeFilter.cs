using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ContentAPI.Fillters
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Diagnostics;

    public class ExecutionTimeFilter : ActionFilterAttribute
    {
        private Stopwatch? _stopwatch;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_stopwatch == null)
                return;

            _stopwatch.Stop();
            Console.WriteLine($"Action {context.ActionDescriptor.DisplayName} took {_stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
