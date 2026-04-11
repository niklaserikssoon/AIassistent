using ContentAPI.DTOs;

namespace ContentAPI.Services
{
    public interface IContentService
    {
        // skapar en request DTO som innehåller prompt och category, och returnerar den skapade request DTO:n
        Task<ResponseDTO> CreateAsync(string promt, string category);
        // hämtar alla request DTOs som finns i databasen och returnerar dem som en lista av response DTOs
        Task<IEnumerable<ResponseDTO>> GetAllAsync(string category);
        // hämtar en request DTO som matchar det angivna id:t och returnerar den som en response DTO
        Task<ResponseDTO> GetByIdAsync(int id);
        // uppdaterar en request DTO som matchar det angivna id:t med de nya värdena från update request DTO:n, och returnerar den uppdaterade request DTO:n som en response DTO
        Task<ResponseDTO> UpdateAsync(int id, UpdateRequestDTO request);
        // tar bort en request DTO som matchar det angivna id:t och returnerar true om borttagningen lyckades, annars false
        Task<bool> DeleteAsync(int id);
    }
}
