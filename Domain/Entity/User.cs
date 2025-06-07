using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Entity;

public class User
{
    private readonly List<Book> _favoriteBooks = new();
    private readonly List<Book> _uploadedBooks = new();

    public Guid Id { get; set; }
    public string UserName { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiry { get; private set; }
    public IReadOnlyCollection<Book> FavoriteBooks => _favoriteBooks.AsReadOnly();
    public IReadOnlyCollection<Book> UploadedBooks => _uploadedBooks.AsReadOnly();

    private User() { }

    public static User Create(
        string userName, 
        string phoneNumber, 
        string passwordHash)
    {
        if (string.IsNullOrEmpty(userName) 
            || string.IsNullOrEmpty(phoneNumber)
            || string.IsNullOrEmpty(passwordHash))
            throw new ArgumentException("Name, password and phone are required.");

        return new User() 
        {
            Id = Guid.NewGuid(),
            UserName = userName,
            PhoneNumber = phoneNumber,
            PasswordHash = passwordHash
        };
    }

    public void UploadBook(Book book)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));
        _uploadedBooks.Add(book);
    }

    public void AddToFavorites(Book book)
    {
        if (book == null) throw new ArgumentNullException(nameof(book));
        if (!_favoriteBooks.Contains(book))
            _favoriteBooks.Add(book);
    }

    public void RemoveFromFavorites(Book book)
        => _favoriteBooks.Remove(book);

    public void UpdateRefreshToken(string token, DateTime expiry)
    {
        RefreshToken = token;
        RefreshTokenExpiry = expiry;
    }

    public void ClearRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiry = null;
    }
}
