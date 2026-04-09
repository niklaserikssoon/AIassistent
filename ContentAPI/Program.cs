using ContentAPI.Data;
using ContentAPI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IContentService, ContentService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ContentDb"));

builder.Services.AddHttpClient("LLMproxy", client =>
{
    client.BaseAddress = new Uri("http://localhost:5008");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
