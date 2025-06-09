using Application.Books.Command.Create;
using Application.Books.Command.Delete;
using Application.Books.Command.Update;
using Application.Books.Query;
using Application.Books.Query.GetBooksByPage;
using Application.Books.Query.GetById;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(IMediator mediator) : Controller
{
    [HttpGet("page")]
    public async Task<IActionResult> GetBooksByPage(
        [FromQuery] int page,
        [FromQuery] int pageSize)
    {
        try
        {
            var (books, totalCount) = await mediator.Send(new GetBooksByPageQuery(page, pageSize));

            return Ok(new
            {
                Books = books,
                TotalCount = totalCount
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookCommand command)
    {
        var bookId = await mediator.Send(command);
        return Ok(bookId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var command = new DeleteBookCommand(id);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook(Guid id)
    {
        var query = new GetBookByIdQuery(id);
        var book = await mediator.Send(query);
        return Ok(book);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBook([FromQuery] TryUpdateBookCommand command) 
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
