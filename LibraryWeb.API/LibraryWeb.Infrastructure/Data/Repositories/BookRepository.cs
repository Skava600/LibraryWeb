using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Contracts.Data.Repositories;
using LibraryWeb.Infrastructure.Data.Repositories.Base;
using LibraryWeb.Migrations;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Infrastructure.Data.Repositories
{
    internal class BookRepository : BaseRepo<Book>, IBookRepository
    {
        public DbSet<Book> Books { get; set; }
        public BookRepository(DatabaseContext context) : base(context)
        {
            Books = context.Books;
        }

        public Book? GetByIsbn(string isbn)
        {
            var book = Books.FirstOrDefault(b => b.ISBN.Equals(isbn));

            return book;
        }
    }
}
