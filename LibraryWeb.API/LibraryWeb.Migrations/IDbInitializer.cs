using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Migrations
{
    public interface IDbInitializer
    {
        Task Seed(IApplicationBuilder app);
    }
}
