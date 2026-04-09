using AIassistent.DTOs;
using AIassistent.services;
using System.Net.Http.Json;

public class PostService : IpostService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public PostService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> PostAsync(string prompt)
    {
        var client = _httpClientFactory.CreateClient("Ollama");

        var requestBody = new
        {
            model = "llama3",
            prompt = prompt,
            stream = false
        };

        var response = await client.PostAsJsonAsync("/api/generate", requestBody);

        var raw = await response.Content.ReadAsStringAsync();

        try
        {
            var result = System.Text.Json.JsonSerializer.Deserialize<OllamaResponseDTO>(raw);

            return result?.response ?? "Inget svar från Ollama";
        }
        catch (System.Text.Json.JsonException ex)
        {
            throw new Exception($"Kunde inte tolka JSON från Ollama. Raw response: {raw}", ex);
        }
    }
}