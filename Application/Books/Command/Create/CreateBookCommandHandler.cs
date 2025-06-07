using Application.Interfaces;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Command.Create;

public class CreateBookCommandHandler(
    IBookRepository repository) 
    : IRequestHandler<CreateBookCommand, Guid>
{
    public async Task<Guid> Handle(CreateBookCommand request, 
        CancellationToken ct)
    {
        var book = Book.Create(
                request.Title,
                request.Author,
                request.Description,
                request.ReleaseDate,
                request.User);
        await repository.AddAsync(book, ct);
        return book.Id;
    }
}
