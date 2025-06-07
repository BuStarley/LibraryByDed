using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Command.Delete;

public record DeleteBookCommand(Guid Id) : IRequest<bool>;
