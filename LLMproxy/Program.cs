using AIassistent.services;
using ContentAPI.Filters;
using LLMproxy.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IpostService, PostService>();
builder.Services.AddScoped<ApiKeyFilter>();

builder.Services.AddHttpClient("OpenAI", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["OpenAI:BaseUrl"]!);
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
        "Bearer", builder.Configuration["OpenAI:ApiKey"]);
    client.Timeout = TimeSpan.FromSeconds(
        builder.Configuration.GetValue<int>("OpenAI:TimeoutSeconds", 30));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
