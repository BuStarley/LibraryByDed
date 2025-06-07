using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Command.Delete;

public class DeleteBookCommandHandler(
    IBookRepository repository)
    : IRequestHandler<DeleteBookCommand, bool>
{
    public async Task<bool> Handle(DeleteBookCommand request, 
        CancellationToken ct) 
        => await repository.TryDeleteByIdAsync(request.Id, ct);
}
