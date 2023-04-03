using Microsoft.AspNetCore.Builder;

namespace LibraryWeb.Migrations
{
    public interface IDbInitializer
    {
        Task Seed(IApplicationBuilder app);
    }
}
