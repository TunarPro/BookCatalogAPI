using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookCatalogLibrary.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public float? Price { get; set; }
    }
}
