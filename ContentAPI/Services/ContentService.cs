using ContentAPI.Data;
using ContentAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace ContentAPI.Services
{
    public class ContentService : IContentService
    {
        private readonly AppDbContext _context;
        // HttpClientFactory för att skapa HttpClient-instans som används för att göra HTTP-förfrågningar till Ollama API
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        public ContentService(AppDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
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
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();

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
            var item = await _context.Contents.FindAsync(id);
            if (item == null)
                return false;

            _context.Contents.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }


        // returnerar alla request DTOs som finns i den interna listan
        public Task<IEnumerable<ResponseDTO>> GetAllAsync(string category)
        {
            var query = _context.Contents.AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(c => c.Category == category);
            }
            var result = query.Select(c => new ResponseDTO
            {
                Id = c.Id,
                Promt = c.Promt,
                Category = c.Category,
                GeneratedText = c.GeneratedText,
                CreatedAt = c.CreatedAt
            }).ToList();
            return Task.FromResult<IEnumerable<ResponseDTO>>(result);
        }


        // returnerar en specifik request DTO baserat på dess id, eller null om den inte hittas
        public Task<ResponseDTO> GetByIdAsync(int id)
        {
            var content = _context.Contents.Find(id);
            if (content == null)
                return Task.FromResult<ResponseDTO>(null);
            return Task.FromResult(new ResponseDTO
            {
                Id = content.Id,
                Promt = content.Promt,
                Category = content.Category,
                GeneratedText = content.GeneratedText,
                CreatedAt = content.CreatedAt
            });
        }
        // uppdaterar en specifik request DTO baserat på dess id och den nya prompten, och returnerar den uppdaterade request DTO:n, eller null om den inte hittas
        public Task<ResponseDTO> UpdateAsync(int id, UpdateRequestDTO request)
        {
            var content = _context.Contents.Find(id);
            if (content == null)
                return Task.FromResult<ResponseDTO>(null);
            content.Promt = request.Promt;
            content.Category = request.Category;
            _context.SaveChanges();
            return Task.FromResult(new ResponseDTO
            {
                Id = content.Id,
                Promt = content.Promt,
                Category = content.Category,
                GeneratedText = content.GeneratedText,
                CreatedAt = content.CreatedAt
            });
        }
    }
}
