using BookCatalogLibrary.Books.Commands;
using BookCatalogLibrary.Data;
using BookCatalogLibrary.DTOs;
using BookCatalogLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.Books.Handlers.CommandHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {
        private readonly IBooksService _booksService;
        public AddBookCommandHandler(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book()
            {
                Title = request.CreateOrUpdateBookDTO.Title,
                Author = request.CreateOrUpdateBookDTO.Author,
                Price = request.CreateOrUpdateBookDTO.Price
            };

            return await _booksService.AddBookAsync(book);
        }
    }
}
