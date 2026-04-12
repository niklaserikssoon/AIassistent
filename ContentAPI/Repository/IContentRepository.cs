using ContentAPI.Data;

namespace ContentAPI.Repository
{
    public interface IContentRepository
    {
        Task<List<AiContent>> GetAllAsync();
        Task<AiContent?> GetByIdAsync(int id);
        Task AddAsync(AiContent content);
        Task<bool> DeleteAsync(int id);
        Task UpdateAsync(AiContent content);
    }
}
