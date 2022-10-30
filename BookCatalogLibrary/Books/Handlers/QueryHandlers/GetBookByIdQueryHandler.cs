using BookCatalogLibrary.Books.Queries;
using BookCatalogLibrary.Data;
using BookCatalogLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.Books.Handlers.QueryHandlers
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBooksService _booksService;

        public GetBookByIdQueryHandler(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return await _booksService.GetBookAsync(request.Id);
        }
    }
}
