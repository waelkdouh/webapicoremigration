using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookServiceCore.Models
{
    public partial class Authors
    {
        public Authors()
        {
            Books = new HashSet<Books>();
        }

        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        public virtual ICollection<Books> Books { get; set; }
    }
}
