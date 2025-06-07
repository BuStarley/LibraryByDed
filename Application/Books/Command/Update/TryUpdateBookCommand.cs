using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Command.Update;

public record TryUpdateBookCommand(
    string Title,
    string Author,
    string Description,
    DateTime ReleaseDate) : IRequest<bool>;