using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LibraryWeb.IdentityServer
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddIdentityServerAuth(this IServiceCollection services, IConfiguration configuration)
        {

            var migrationAssembly = Assembly.GetExecutingAssembly().GetName().Name;
            var connectionString = configuration.GetConnectionString("DataAccessPostgreSqlProvider");
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentityServer()
                .AddConfigurationStore(options => options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                opt => opt.MigrationsAssembly(migrationAssembly)))

                .AddOperationalStore(options => options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString,
                opt => opt.MigrationsAssembly(migrationAssembly)))

                .AddAspNetIdentity<User>()
                .AddDeveloperSigningCredential();
            services.AddAuthentication();
            return services;
        }
    }
}
