using AIassistent.DTOs;
using AIassistent.services;
using System.Net.Http.Json;

public class PostService : IpostService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public PostService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<string> PostAsync(string prompt)
    {
        var client = _httpClientFactory.CreateClient("OpenAI");
        var endpoint = _configuration["OpenAI:Endpoint"]!;
        var model = _configuration["OpenAI:Model"]!;

        var requestBody = new
        {
            model,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var response = await client.PostAsJsonAsync(endpoint, requestBody);
        response.EnsureSuccessStatusCode();

        var raw = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(raw))
            throw new Exception("Tomt svar från OpenAI");

        var result = System.Text.Json.JsonSerializer.Deserialize<OpenAiResponseDTO>(raw);
        var content = result?.choices?.FirstOrDefault()?.message?.content;

        if (string.IsNullOrWhiteSpace(content))
            throw new Exception("Oväntat svar från OpenAI: inget innehåll i choices");

        return content;
    }
}
