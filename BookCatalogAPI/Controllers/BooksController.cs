using BookCatalogLibrary.Books.Commands;
using BookCatalogLibrary.Books.Queries;
using BookCatalogLibrary.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> AddBook(CreateOrUpdateBookDTO createOrUpdateBook)
        {
            try
            {
                await _mediator.Send(new AddBookCommand(createOrUpdateBook));
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBook(int id, CreateOrUpdateBookDTO createOrUpdateBook)
        {
            try
            {
                var book = await _mediator.Send(new GetBookByIdQuery(id));
                if (book == null)
                    return BadRequest();

                await _mediator.Send(new UpdateBookCommand(id, createOrUpdateBook));
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _mediator.Send(new GetBookByIdQuery(id));
                if (book == null)
                    return BadRequest();

                await _mediator.Send(new DeleteBookCommand(id));
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}