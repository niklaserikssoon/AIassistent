using ContentAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ContentAPI.Repository
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _context;

        public ContentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AiContent>> GetAllAsync()
            => await _context.Contents.ToListAsync();

        public async Task AddAsync(AiContent content)
        {
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content == null)
                return false;
            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<AiContent?> GetByIdAsync(int id)
        {
            return _context.Contents.FindAsync(id).AsTask();
        }

        public Task UpdateAsync(AiContent content)
        {
            _context.Contents.Update(content);
            return _context.SaveChangesAsync();
        }

        public Task<List<AiContent>> GetAllAsync(string category)
        {
            return _context.Contents.Where(c => c.Category == category).ToListAsync();
        }
    }
}
