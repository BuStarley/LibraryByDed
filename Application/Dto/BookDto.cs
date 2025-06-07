using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto;

public record BookDto(
    Guid Id,
    string Title,
    string Author,
    string Description,
    DateTime ReleaseDate);