using LibraryWeb.Contracts.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraryWeb.Migrations
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddIdentityServiceAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationAssembly = Assembly.GetExecutingAssembly().GetName().Name;
            var connectionString = configuration.GetConnectionString("DataAccessPostgreSqlProvider");
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<DatabaseContext>()
               .AddDefaultTokenProviders();
            services.AddIdentityServer()
                .AddConfigurationStore(options => options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                opt => opt.MigrationsAssembly(migrationAssembly)))

                .AddOperationalStore(options => options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                opt => opt.MigrationsAssembly(migrationAssembly)))

                .AddAspNetIdentity<User>() 
                .AddDeveloperSigningCredential();
            return services;
        }
    }
}
