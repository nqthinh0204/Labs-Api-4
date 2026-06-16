using AspLab04.Mvc.Models;
using Microsoft.EntityFrameworkCore;

namespace AspLab04.Mvc.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genres");
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");
            entity.HasKey(b => b.Id);
            entity.Property(b => b.BookCode).IsRequired().HasMaxLength(20);
            entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Author).IsRequired().HasMaxLength(150);
            entity.Property(b => b.Publisher).HasMaxLength(150);
            entity.Property(b => b.Price).HasColumnType("decimal(18,2)");
            entity.HasOne(b => b.Genre)
                  .WithMany(g => g.Books)
                  .HasForeignKey(b => b.GenreId);
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sales");
            entity.HasKey(s => s.Id);
            entity.Property(s => s.CustomerName).HasMaxLength(150);
            entity.Property(s => s.TotalAmount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.ToTable("SaleItems");
            entity.HasKey(si => si.Id);
            entity.Property(si => si.UnitPrice).HasColumnType("decimal(18,2)");
            entity.HasOne(si => si.Sale)
                  .WithMany(s => s.SaleItems)
                  .HasForeignKey(si => si.SaleId);
            entity.HasOne(si => si.Book)
                  .WithMany(b => b.SaleItems)
                  .HasForeignKey(si => si.BookId);
        });

        // Seed Genres
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Programming" },
            new Genre { Id = 2, Name = "Science" },
            new Genre { Id = 3, Name = "Novel" },
            new Genre { Id = 4, Name = "Self Development" }
        );

        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                BookCode = "BK001",
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Publisher = "Prentice Hall",
                Price = 250000,
                Quantity = 10,
                MinStock = 3,
                GenreId = 1
            },

            new Book
            {
                Id = 2,
                BookCode = "BK002",
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                Publisher = "Addison Wesley",
                Price = 280000,
                Quantity = 8,
                MinStock = 3,
                GenreId = 1
            },

            new Book
            {
                Id = 3,
                BookCode = "BK003",
                Title = "Design Patterns",
                Author = "Erich Gamma",
                Publisher = "Addison Wesley",
                Price = 350000,
                Quantity = 5,
                MinStock = 2,
                GenreId = 1
            },

            new Book
            {
                Id = 4,
                BookCode = "BK004",
                Title = "A Brief History of Time",
                Author = "Stephen Hawking",
                Publisher = "Bantam Books",
                Price = 180000,
                Quantity = 7,
                MinStock = 2,
                GenreId = 2
            },

            new Book
            {
                Id = 5,
                BookCode = "BK005",
                Title = "Sapiens",
                Author = "Yuval Noah Harari",
                Publisher = "Harper",
                Price = 220000,
                Quantity = 4,
                MinStock = 2,
                GenreId = 2
            },

            new Book
            {
                Id = 6,
                BookCode = "BK006",
                Title = "Harry Potter",
                Author = "J.K. Rowling",
                Publisher = "Bloomsbury",
                Price = 200000,
                Quantity = 15,
                MinStock = 5,
                GenreId = 3
            },

            new Book
            {
                Id = 7,
                BookCode = "BK007",
                Title = "Atomic Habits",
                Author = "James Clear",
                Publisher = "Avery",
                Price = 230000,
                Quantity = 6,
                MinStock = 3,
                GenreId = 4
            },

            new Book
            {
                Id = 8,
                BookCode = "BK008",
                Title = "The 7 Habits",
                Author = "Stephen Covey",
                Publisher = "Free Press",
                Price = 210000,
                Quantity = 3,
                MinStock = 3,
                GenreId = 4
            }
        );
    }
}