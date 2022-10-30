using BookCatalogLibrary.Data;
using BookCatalogLibrary.DTOs;
using BookCatalogLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.Books.Queries
{
    public record GetAllBooksQuery() : IRequest<IEnumerable<Book>>;
}
