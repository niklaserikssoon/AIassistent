using ContentAPI.Data;
using ContentAPI.DTOs;
using ContentAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Net.Http;

namespace ContentAPI.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ContentService(IContentRepository repository,IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        // skapar en request DTO som innehåller prompt och category, och returnerar den skapade request DTO:n
        public async Task<ResponseDTO> CreateAsync(string promt, string category)
        {
            var client = _httpClientFactory.CreateClient("LLMproxy");

            var apiKey = _configuration["ApiSettings:InternalApiKey"];

            client.DefaultRequestHeaders.Remove("X-API-KEY");
            client.DefaultRequestHeaders.Add("X-API-KEY", apiKey);


            // pekar på controller routen i LLMproxy API
            var response = await client.PostAsJsonAsync("/api/ollama", new
            {
                Promt = promt,
            });
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResponseDTO>();

            var content = new AiContent
            {
                Promt = promt,
                Category = category,
                GeneratedText = result.GeneratedText,
                CreatedAt = DateTime.UtcNow
            };
            await _repository.AddAsync(content);

            return new ResponseDTO
            {
                Id = content.Id,
                Promt = content.Promt,
                Category = content.Category,
                GeneratedText = content.GeneratedText,
                CreatedAt = content.CreatedAt
            };
        }

        // tar bort en request DTO baserat på dess id, och returnerar true om borttagningen lyckades, annars false
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }


        // returnerar alla request DTOs som finns i den interna listan
        public async Task<IEnumerable<ResponseDTO>> GetAllAsync(string category)
        {

            var contents = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(category))
            {
                contents = contents
                    .Where(c => c.Category == category)
                    .ToList();
            }
            return contents.Select(c => new ResponseDTO
            {
                Id = c.Id,
                Promt = c.Promt,
                Category = c.Category,
                GeneratedText = c.GeneratedText,
                CreatedAt = c.CreatedAt
            });
        }


        // returnerar en specifik request DTO baserat på dess id, eller null om den inte hittas
        public async Task<ResponseDTO> GetByIdAsync(int id)
        {
            var content = await _repository.GetByIdAsync(id);
            if (content == null)
            {
                return null;
            }
            return new ResponseDTO
            {
                Id = content.Id,
                Promt = content.Promt,
                Category = content.Category,
                GeneratedText = content.GeneratedText,
                CreatedAt = content.CreatedAt
            };
        }
        // uppdaterar en specifik request DTO baserat på dess id och den nya prompten, och returnerar den uppdaterade request DTO:n, eller null om den inte hittas
        public async Task<ResponseDTO> UpdateAsync(int id, UpdateRequestDTO request)
        {
            var content = await _repository.GetByIdAsync(id);
            if (content == null)
            {
                return null;
            }
            content.Promt = request.Promt;
            content.Category = request.Category;
            await _repository.UpdateAsync(content);
            return new ResponseDTO
            {
                Id = content.Id,
                Promt = content.Promt,
                Category = content.Category,
                GeneratedText = content.GeneratedText,
                CreatedAt = content.CreatedAt
            };
        }
    }
}
