namespace AIassistent.DTOs
{
    public class OpenAiResponseDTO
    {
        public List<Choice> choices { get; set; } = [];

        public class Choice
        {
            public Message message { get; set; } = new();
        }

        public class Message
        {
            public string content { get; set; } = string.Empty;
        }
    }
}
