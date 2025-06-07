using Application.Books.Command.Create;
using Application.Books.Command.Delete;
using Application.Books.Command.Update;
using Application.Books.Query;
using Application.Books.Query.GetById;
using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/books")]
public class BookController(IMediator mediator) : Controller
{

    [HttpPost]
    public async Task<IActionResult> CreateBook(CreateBookCommand command)
    {
        var bookId = await mediator.Send(command);
        return Ok(bookId);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        var command = new DeleteBookCommand(id);
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetBook(Guid id)
    {
        var query = new GetBookByIdQuery(id);
        var book = await mediator.Send(query);
        return Ok(book);
    }

    [HttpPut]
    public async Task<IActionResult> Update(TryUpdateBookCommand command) 
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
