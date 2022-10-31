using BookCatalogLibrary.Models;

namespace BookCatalogLibrary.Services
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
