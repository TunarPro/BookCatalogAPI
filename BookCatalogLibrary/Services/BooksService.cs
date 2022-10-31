using BookCatalogLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalogLibrary.Data
{
    public class BooksService : IBooksService
    {
        private readonly BookDBContext _context;

        public BooksService(BookDBContext context)
        {
            _context = context;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<int> DeleteBookAsync(Book book)
        {
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }

        public async Task<int> UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            return await _context.SaveChangesAsync();
        }
    }
}
