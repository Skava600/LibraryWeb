using LibraryWeb.Contracts.Data;
using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Infrastructure.Data;
using LibraryWeb.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraryWeb.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }
        
        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DataAccessPostgreSqlProvider");
           
            return services.AddNpgsql<DatabaseContext>(connectionString);
        }
    }
}
