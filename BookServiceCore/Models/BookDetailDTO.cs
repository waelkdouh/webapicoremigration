
using System.Text.Json.Serialization;

namespace BookServiceCore.Models
{
    public class BookDetailDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("Year")]
        public int Year { get; set; }
        
        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
        
        [JsonPropertyName("AuthorName")]
        public string AuthorName { get; set; }

        [JsonPropertyName("Genre")]
        public string Genre { get; set; }
    }
}