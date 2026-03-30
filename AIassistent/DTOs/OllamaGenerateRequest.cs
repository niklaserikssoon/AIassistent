namespace AIassistent.DTOs
{
    public class OllamaGenerateRequest
    {
        public string Model { get; set; } = "llama3";
        public string Prompt { get; set; } = string.Empty;
        public bool Stream { get; set; } = false;
    }
}
