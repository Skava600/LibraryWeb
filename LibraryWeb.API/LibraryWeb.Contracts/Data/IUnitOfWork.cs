using LibraryWeb.Contracts.Data.Repositories;

namespace LibraryWeb.Contracts.Data
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        //IUserRepository Users { get; }
        Task CommitAsync();
    }
}
