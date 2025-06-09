using Application.Interfaces;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Query.GetBooksByPage;

public class GetBooksByPageQueryHandler(
    IBookRepository repository)
    : IRequestHandler<GetBooksByPageQuery, (List<Book>, int)>
{
    public async Task<(List<Book>, int)> Handle(
        GetBooksByPageQuery request, 
        CancellationToken ct)
    {
        int skip = (request.Page - 1) * request.PageSize;

        var books = await repository.GetAllAsync(ct);
        int count = books.Count;

        books = books
            .OrderBy(b => b.Id)
            .Skip(skip)
            .Take(request.PageSize)
            .ToList();

        return (books, count);
    }
}
