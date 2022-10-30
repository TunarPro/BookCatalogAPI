using BookCatalogLibrary.Data;
using BookCatalogLibrary.DTOs;
using BookCatalogLibrary.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.Books.Commands
{
    public record AddBookCommand(CreateOrUpdateBookDTO CreateOrUpdateBookDTO) : IRequest<Book>;
}
