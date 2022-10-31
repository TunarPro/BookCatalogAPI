using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.Models
{
    public class AuthenticateRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username length can be min 3 and max 20")]
        public string Username { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 8, ErrorMessage = "Password length can be min 8 and max 35")]
        public string Password { get; set; }
    }
}
