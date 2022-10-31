using System.ComponentModel.DataAnnotations;

namespace BookCatalogLibrary.Models
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username length can be min 3 and max 20")]
        public string Username { get; set; }
        [Required]
        [StringLength(35, MinimumLength = 8, ErrorMessage = "Password length can be min 8 and max 35")]
        public string Password { get; set; }
        // Because there is not an option of choosing from select option I've provided 'Role' as a string ****
        [Required]
        public string Role { get; set; }
        [Required]
        public string GivenName { get; set; }
    }
}
