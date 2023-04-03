using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using LibraryWeb.Contracts.Data.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace LibraryWeb.Migrations
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(DatabaseContext applicationDbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed(IApplicationBuilder app)
        {
            _context.Database.EnsureCreated();

            PopulateIdentityServer(app);
            EnsureUsers();

            if (!_context.Books.Any())
            {
                var book = new Book
                {
                    Author = "Andrzej Sapkowski",
                    Name = "The last wish",
                    Description = "The Last Wish is a collection of six short stories surrounding The Witcher, Geralt of Rivia, and they are intersected by a frame story.",
                    Genre = "Fantasy",
                    ISBN = "9780316029186",
                    DateIssued = new DateOnly(2022, 12, 1),
                    DateDue = new DateOnly(2023, 1, 26),
                };


                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
            }
        }

        private void EnsureUsers()
        {

            var alice = _userManager.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new User
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                };
                var result = _userManager.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = _userManager.AddClaimsAsync(alice, new Claim[]
                        {
                  new Claim(JwtClaimTypes.Name, "Alice Smith"),
                  new Claim(JwtClaimTypes.GivenName, "Alice"),
                  new Claim(JwtClaimTypes.FamilyName, "Smith"),
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }
        public void PopulateIdentityServer(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
