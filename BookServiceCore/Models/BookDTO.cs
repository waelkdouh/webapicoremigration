
using System.Text.Json.Serialization;

namespace BookServiceCore.Models
{
    public class BookDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
        
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        
        [JsonPropertyName("AuthorName")]
        public string AuthorName { get; set; }
    }
}