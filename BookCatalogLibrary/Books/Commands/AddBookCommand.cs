using BookCatalogLibrary.DTOs;
using BookCatalogLibrary.Models;
using MediatR;

namespace BookCatalogLibrary.Books.Commands
{
    public record AddBookCommand(CreateOrUpdateBookDTO CreateOrUpdateBookDTO) : IRequest<Book>;
}
