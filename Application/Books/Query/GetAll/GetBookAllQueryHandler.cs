using Application.Interfaces;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Query.GetAll;

public class GetBookAllQueryHandler(
    IBookRepository repository)
    : IRequestHandler<GetBookAllQuery, List<Book>>
{
    public Task<List<Book>> Handle(GetBookAllQuery request,
        CancellationToken ct)
        => repository.GetAllAsync(ct);
}
