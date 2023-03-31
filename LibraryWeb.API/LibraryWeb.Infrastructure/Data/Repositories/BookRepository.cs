using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Contracts.Data.Repositories;
using LibraryWeb.Infrastructure.Data.Repositories.Base;
using LibraryWeb.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Infrastructure.Data.Repositories
{
    internal class BookRepository : BaseRepo<Book>, IBookRepository
    {
        public BookRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
