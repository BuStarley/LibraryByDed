using Application.Books.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Query.GetById;

public record GetBookByIdQuery(Guid Id) : IRequest<BookDto>;
