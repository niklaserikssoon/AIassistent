using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LLMproxy.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                _logger.LogError("AI service rejected the request: invalid API key.");
                await WriteProblem(context, 502, "AI Service Error", "Could not authenticate with the AI service.");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests)
            {
                _logger.LogError("AI service rate limit hit.");
                await WriteProblem(context, 429, "Too Many Requests", "The AI service is currently overloaded. Please try again later.");
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "AI service request timed out.");
                await WriteProblem(context, 504, "Gateway Timeout", "The AI service did not respond in time. Please try again later.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await WriteProblem(context, 500, "Internal Server Error", "An unexpected error occurred.");
            }
        }

        private static async Task WriteProblem(HttpContext context, int status, string title, string detail)
        {
            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail
            };
            context.Response.StatusCode = status;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
