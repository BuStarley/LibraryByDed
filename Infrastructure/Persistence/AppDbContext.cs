using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(book =>
        {
            book.ToTable("Books");
            book.HasKey(b => b.Id);

            book.Property(b => b.Title).HasMaxLength(128).IsRequired();
            book.Property(b => b.Description).HasMaxLength(2048);
            book.Property(b => b.Author).HasMaxLength(128);

            book.HasOne(b => b.UploadedByUser)
            .WithMany(u => u.UploadedBooks)
            .HasForeignKey(b => b.UploadedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(user =>
        {
            user.ToTable("Users");
            user.HasKey(u => u.Id);

            user.HasMany(u => u.FavoriteBooks)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserFavoriteBooks",
                    j => j.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasKey("UserId", "BookId")
                );

            user.Property(u => u.UserName).HasMaxLength(64).IsRequired();
            user.Property(u => u.PhoneNumber).HasMaxLength(20).IsRequired();
            user.Property(u => u.PasswordHash).HasMaxLength(256).IsRequired();
        });
    }
}
