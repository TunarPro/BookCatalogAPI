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
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
    {
        private readonly IBooksService _booksService;

        public GetAllBooksQueryHandler(IBooksService booksService)
        {
            _booksService = booksService;
        }

        public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return await _booksService.GetBooksAsync();
        }
    }
}
