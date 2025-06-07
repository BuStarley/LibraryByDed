using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Author { get; set; }
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }

    private Book() { }

    public static Book Create(
        string title, string? author, string? description, DateTime? releaseDate)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("title cannot be empty");

        return new Book() 
        {
            Id = Guid.NewGuid(),
            Title = title,
            Author = author,
            Description = description,
            ReleaseDate = releaseDate
        };
    }
}
