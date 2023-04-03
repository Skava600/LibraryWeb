using LibraryWeb.Contracts.Data.Entities;
using Microsoft.AspNetCore.Builder;

namespace LibraryWeb.Migrations
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DatabaseContext _context;
        public DbInitializer(DatabaseContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task Seed(IApplicationBuilder app)
        {
            _context.Database.EnsureCreated();

            if (!_context.Books.Any())
            {
                var book = new Book
                {
                    Author = "Andrzej Sapkowski",
                    Name = "The last wish",
                    Description = "The Last Wish is a collection of six short stories surrounding The Witcher, Geralt of Rivia, and they are intersected by a frame story.",
                    Genre = "Fantasy",
                    ISBN = "9780316029186",
                    DateIssued = new DateOnly(2022, 12, 1),
                    DateDue = new DateOnly(2023, 1, 26),
                };


                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
            }
        }


    }
}
