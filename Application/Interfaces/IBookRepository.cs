namespace Application.Interfaces;
using Domain.Entity;
using System.Collections.Generic;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<bool> TryDeleteByIdAsync(Guid id, CancellationToken ct);
    Task AddAsync(Book book, CancellationToken ct);
    Task<bool> TryUpdateAsync(Book book, CancellationToken ct);
    Task<List<Book>> GetAllAsync(CancellationToken ct);
}