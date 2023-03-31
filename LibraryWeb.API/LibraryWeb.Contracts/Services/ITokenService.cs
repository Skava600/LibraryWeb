using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Contracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Contracts.Services
{
    public interface ITokenService
    {
        AuthTokenDTO Generate(User user);
    }
}
