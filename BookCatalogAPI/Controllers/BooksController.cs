using BookCatalogLibrary.Books.Commands;
using BookCatalogLibrary.Books.Queries;
using BookCatalogLibrary.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetById(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            if (book == null)
                return NotFound($"There isn't a book with the ID \"{id}\"");

            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult> Add(CreateOrUpdateBookDTO createOrUpdateBook)
        {
            try
            {
                await _mediator.Send(new AddBookCommand(createOrUpdateBook));
                return Ok(createOrUpdateBook);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<ActionResult> Update(int id, CreateOrUpdateBookDTO createOrUpdateBook)
        {
            try
            {
                var book = await _mediator.Send(new GetBookByIdQuery(id));
                if (book == null)
                    return BadRequest("Update operation wasn't possible. (Invalid ID)");

                await _mediator.Send(new UpdateBookCommand(id, createOrUpdateBook));
                return Ok(createOrUpdateBook);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var book = await _mediator.Send(new GetBookByIdQuery(id));
                if (book == null)
                    return BadRequest("Delete operation wasn't possible. (Invalid ID)");

                await _mediator.Send(new DeleteBookCommand(id));
                return Ok($"Successfully deleted the book with the ID \"{id}\"");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}