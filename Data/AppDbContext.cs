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
            new Genre { Id = 1, Name = "Kỹ năng sống" },
            new Genre { Id = 2, Name = "Tiểu thuyết" },
            new Genre { Id = 3, Name = "Khoa học" },
            new Genre { Id = 4, Name = "Công nghệ" }
        );

        // Seed Books
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, BookCode = "BK-SKL-001", Title = "Đắc Nhân Tâm", Author = "Dale Carnegie", Publisher = "NXB Tổng hợp TP.HCM", Price = 88000, Quantity = 25, MinStock = 5, GenreId = 1 },
            new Book { Id = 2, BookCode = "BK-SKL-002", Title = "Tư Duy Nhanh Và Chậm", Author = "Daniel Kahneman", Publisher = "NXB Lao Động", Price = 120000, Quantity = 3, MinStock = 5, GenreId = 1 },
            new Book { Id = 3, BookCode = "BK-NOV-001", Title = "Nhà Giả Kim", Author = "Paulo Coelho", Publisher = "NXB Hội Nhà Văn", Price = 75000, Quantity = 10, MinStock = 3, GenreId = 2 },
            new Book { Id = 4, BookCode = "BK-NOV-002", Title = "Sapiens: Lược Sử Loài Người", Author = "Yuval Noah Harari", Publisher = "NXB Tri Thức", Price = 175000, Quantity = 2, MinStock = 3, GenreId = 2 },
            new Book { Id = 5, BookCode = "BK-SCI-001", Title = "Lược Sử Thời Gian", Author = "Stephen Hawking", Publisher = "NXB Trẻ", Price = 120000, Quantity = 0, MinStock = 3, GenreId = 3 },
            new Book { Id = 6, BookCode = "BK-TECH-001", Title = "Clean Code", Author = "Robert C. Martin", Publisher = "NXB Lao Động", Price = 220000, Quantity = 8, MinStock = 3, GenreId = 4 },
            new Book { Id = 7, BookCode = "BK-TECH-002", Title = "The Pragmatic Programmer", Author = "David Thomas", Publisher = "NXB Lao Động", Price = 195000, Quantity = 4, MinStock = 3, GenreId = 4 }
        );
    }
}