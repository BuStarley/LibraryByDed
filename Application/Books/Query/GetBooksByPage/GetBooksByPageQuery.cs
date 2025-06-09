using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Query.GetBooksByPage;

public record GetBooksByPageQuery(
    int Page, 
    int PageSize) : IRequest<(List<Book>, int)>;
