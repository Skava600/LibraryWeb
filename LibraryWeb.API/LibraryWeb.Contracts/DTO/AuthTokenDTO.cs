using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Contracts.DTO
{
    public class AuthTokenDTO
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
