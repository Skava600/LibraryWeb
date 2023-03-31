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
    internal class UserRepository : BaseRepo<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
