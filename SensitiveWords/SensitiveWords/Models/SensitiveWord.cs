namespace SensitiveWords.Models
{
    public class SensitiveWord
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? Word { get; set; }
    }
}
