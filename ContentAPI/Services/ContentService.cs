using ContentAPI.DTOs;
using System.Net.Http;
using ContentAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ContentAPI.Services
{
    public class ContentService : IContentService
    {
        private readonly AppDbContext _context;
        // HttpClientFactory för att skapa HttpClient-instans som används för att göra HTTP-förfrågningar till Ollama API
        private readonly IHttpClientFactory _httpClientFactory;

        public ContentService(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        // skapar en request DTO som innehåller prompt och category, och returnerar den skapade request DTO:n
        public async Task<ResponseDTO> CreateAsync(string promt, string category)
        {
            var client = _httpClientFactory.CreateClient("LLMproxy");

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
        public Task<IEnumerable<ResponseDTO>> GetAllAsync()
        {
            return Task.FromResult(_context.Contents.Select(c => new ResponseDTO
            {
                Id = c.Id,
                Promt = c.Promt,
                Category = c.Category,
                GeneratedText = c.GeneratedText,
                CreatedAt = c.CreatedAt
            }).AsEnumerable());
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
