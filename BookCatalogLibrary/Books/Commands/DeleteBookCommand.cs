using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogLibrary.Books.Commands
{
    public record DeleteBookCommand(int Id) : IRequest<int>;
}
