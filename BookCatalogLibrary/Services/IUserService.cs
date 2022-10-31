using BookCatalogLibrary.Models;

namespace BookCatalogLibrary.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
        Task<User> GetUser(int id);
        Task<User> RegisterUser(RegisterRequest request);
        Task<User> UpdateUser(int id, UpdateRequest request);
        Task DeleteUser(int id);
    }
}
