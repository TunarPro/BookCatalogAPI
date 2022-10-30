using BookCatalogLibrary.Books.Commands;
using BookCatalogLibrary.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.Books.Handlers.CommandHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, int>
    {
        private readonly IBooksService _booksService;
        public DeleteBookCommandHandler(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public async Task<int> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = await _booksService.GetBookAsync(request.Id);

            return await _booksService.DeleteBookAsync(bookToUpdate);
        }
    }
}
