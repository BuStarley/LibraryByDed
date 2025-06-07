using Application.Dto;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Query.GetById;

public class GetBookByIdQueryHundler : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IBookRepository _repository;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHundler(IBookRepository repository, IMapper mapper)
        => (_repository, _mapper) = (repository, mapper);

    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken ct)
    {
        var book = await _repository.GetByIdAsync(request.Id, ct);
        return _mapper.Map<BookDto>(book);
    }
}
