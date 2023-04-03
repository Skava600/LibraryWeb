using LibraryWeb.Contracts.Data.Entities;

namespace LibraryWeb.Contracts.Data.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        int Count();
    }
}
