using AspLab04.Mvc.Data;
using AspLab04.Mvc.Options;
using AspLab04.Mvc.Repositories;
using AspLab04.Mvc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ISaleService, SaleService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();