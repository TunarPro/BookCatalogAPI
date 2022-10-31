namespace BookCatalogLibrary.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string GivenName { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
    }
}
