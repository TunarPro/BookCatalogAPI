using BookCatalogLibrary.Models;

namespace BookCatalogLibrary.Data
{
    public interface IBooksService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task<int> UpdateBookAsync(Book book);
        Task<int> DeleteBookAsync(Book book);
    }
}
