using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Query.GetBooksByPage;

public class GetBooksByPageQueryHandler(
    IBookRepository repository,
    IMapper mapper)
    : IRequestHandler<GetBooksByPageQuery, (List<BookDto>, int)>
{
    public async Task<(List<BookDto>, int)> Handle(
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

        var mapBooks = new List<BookDto>();

        foreach (var book in books)
        {
            mapBooks.Add(mapper.Map<BookDto>(book));
        }


        return (mapBooks, count);
    }
}
