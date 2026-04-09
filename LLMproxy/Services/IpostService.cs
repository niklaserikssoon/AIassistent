namespace AIassistent.services
{
    public interface IpostService
    {
        Task<string> PostAsync(string prompt);
    }
}
