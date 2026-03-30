using AIassistent.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AIassistent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LlmController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LlmController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateRequest request)
        {
            var client = _httpClientFactory.CreateClient("Ollama");

            var ollamaRequest = new OllamaGenerateRequest
            {
                Model = "llama3",
                Prompt = request.Promt,
                Stream = false
            };

            var httpResponse = await client.PostAsJsonAsync("api/generate", ollamaRequest);
            httpResponse.EnsureSuccessStatusCode();

            var ollamaResponse = await httpResponse.Content.ReadFromJsonAsync<OllamaGenerateResponse>();

            return Ok(new GenerateResponse
            {
                GeneratedText = ollamaResponse?.Response ?? string.Empty
            });

        }
    }
}
