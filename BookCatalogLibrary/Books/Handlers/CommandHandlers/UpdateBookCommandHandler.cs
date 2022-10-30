using BookCatalogLibrary.Books.Commands;
using BookCatalogLibrary.Data;
using BookCatalogLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.Books.Handlers.CommandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, int>
    {
        private readonly IBooksService _booksService;
        public UpdateBookCommandHandler(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public async Task<int> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = await _booksService.GetBookAsync(request.Id);

            bookToUpdate.Title = request.CreateOrUpdateBookDTO.Title;
            bookToUpdate.Author = request.CreateOrUpdateBookDTO.Author;
            bookToUpdate.Price = request.CreateOrUpdateBookDTO.Price;

            return await _booksService.UpdateBookAsync(bookToUpdate);
        }
    }
}
