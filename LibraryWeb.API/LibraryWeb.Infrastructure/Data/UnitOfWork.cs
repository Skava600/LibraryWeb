using LibraryWeb.Contracts.Data;
using LibraryWeb.Contracts.Data.Repositories;
using LibraryWeb.Infrastructure.Data.Repositories;
using LibraryWeb.Migrations;

namespace LibraryWeb.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        public IBookRepository Books => new BookRepository(_context);
        public IUserRepository Users => new UserRepository(_context);


        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
