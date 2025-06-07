using Application.Interfaces;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Command.Update;

public class TryUpdateBookCommandHandler(
    IBookRepository repository)
    : IRequestHandler<TryUpdateBookCommand, bool>
{
    public async Task<bool> Handle(TryUpdateBookCommand request, 
        CancellationToken ct)
    {
        return await repository.TryUpdateAsync(Book.Create(
            request.Title,
            request.Author,
            request.Description,
            request.ReleaseDate,
            request.User), ct);
    }
}
