using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity;

public class Book
{
    public Guid Id { get; private set; }
    public Guid UploadedByUserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Author { get; private set; }
    public string? Description { get; private set; }
    public DateTime? ReleaseDate { get; private set; }
    public User UploadedByUser { get; private set; }

    private Book() { }

    public static Book Create(
        string title,
        string? author, 
        string? description,
        DateTime? releaseDate,
        User user)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("title cannot be empty");

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var book = new Book()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Author = author,
            Description = description,
            ReleaseDate = releaseDate,
            UploadedByUserId = user.Id,
            UploadedByUser = user
        };
        user.UploadBook(book);
        return book;
    }
}
