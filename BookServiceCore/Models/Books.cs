using System;
using System.Collections.Generic;

namespace BookServiceCore.Models
{
    public partial class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public int AuthorId { get; set; }

        public virtual Authors Author { get; set; }
    }
}
