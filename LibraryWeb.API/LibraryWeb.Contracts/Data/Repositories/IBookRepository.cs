using LibraryWeb.Contracts.Data.Entities;

namespace LibraryWeb.Contracts.Data.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Book? GetByIsbn(string isbn);
    }
}
