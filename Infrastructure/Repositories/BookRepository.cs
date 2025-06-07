using Application.Interfaces;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories;

public class BookRepository(BookDbContext db) : IBookRepository
{
    public async Task AddAsync(Book book, CancellationToken ct)
    {
        await db.Books.AddAsync(book);
        await db.SaveChangesAsync(ct);
    }

    public async Task<List<Book>> GetAllAsync(CancellationToken ct)
        => await db.Books.ToListAsync(ct);

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken ct) 
        => await db.Books.FindAsync(id, ct);

    public async Task<bool> TryDeleteByIdAsync(Guid id, CancellationToken ct)
    {
        var bookFind = await db.Books.FindAsync(id, ct);

        if (bookFind == null)
            return false;

        db.Books.Remove(bookFind);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> TryUpdateAsync(Book book, CancellationToken ct)
    {
        var bookFind = await db.Books.FindAsync(book.Id, ct);

        if (bookFind == null) 
            return false;

        db.Books.Entry(bookFind).CurrentValues.SetValues(book);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
