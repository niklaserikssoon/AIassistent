namespace ContentAPI.DTOs
{
    public class ResponseDTO
    {
        public int Id { get; set; }
        public string Promt { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string GeneratedText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
