using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.DTOs
{
    public record BookDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Title must have min length of 5 and max Length of 50")]
        public string Title { get; set; }
        [Required]
        [StringLength(35, MinimumLength = 3, ErrorMessage = "Author must have min length of 3 and max Length of 35")]
        public string Author { get; set; }
        [Required]
        [Range(1, 100)]
        public float? Price { get; set; }
    }
}
