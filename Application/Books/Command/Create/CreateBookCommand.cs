﻿using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Books.Command.Create;

public record CreateBookCommand(
    string Title,
    string Author,
    string Description,
    DateTime ReleaseDate,
    User User) : IRequest<Guid>;