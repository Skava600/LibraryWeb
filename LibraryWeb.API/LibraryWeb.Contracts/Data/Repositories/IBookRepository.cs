using LibraryWeb.Contracts.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Contracts.Data.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Book? GetByIsbn(string  isbn);
    }
}
