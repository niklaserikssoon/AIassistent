using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContentAPI.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class ApiKeyFilter : Attribute, IAsyncActionFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // hämtar korrekt API-nyckel
            var expectedApiKey = _configuration["ApiSettings:InternalApiKey"];

            // kollar om den finns
            if (!context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var providedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("API key missing");
                return;
            }

            if (providedApiKey != expectedApiKey)
            {
                context.Result = new UnauthorizedObjectResult("Invalid API key");
                return;
            }
            // Om API-nyckeln är giltig, fortsätt till nästa steg i pipeline
            await next();
        }
    }
}
