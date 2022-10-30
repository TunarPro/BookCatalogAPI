using BookCatalogLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
