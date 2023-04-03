using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace LibraryWeb.Migrations
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1144",
                Username = "mukesh",
                Password = "mukesh",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Mukesh Murugan"),
                    new Claim(JwtClaimTypes.GivenName, "Mukesh"),
                    new Claim(JwtClaimTypes.FamilyName, "Murugan"),
                    new Claim(JwtClaimTypes.WebSite, "http://codewithmukesh.com"),
                }
            }
};
        public static List<Client> Clients = new List<Client>
        {
                new Client
                {
                    ClientId = "identity-server-demo-web",
                    AllowedGrantTypes = new List<string> { GrantType.AuthorizationCode },
                    RequireClientSecret = false,
                    RequireConsent = false,
                    RedirectUris = new List<string> { "http://localhost:3006/signin-callback.html" },
                    PostLogoutRedirectUris = new List<string> { "http://localhost:3006/" },
                    AllowedScopes = { "identity-server-demo-api", "write", "read", "openid", "profile", "email" },
                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:3006",
                    },
                    AccessTokenLifetime = 86400
                }
        };
        public static List<ApiResource> ApiResources = new List<ApiResource>
        {
            new ApiResource
            {
                Name = "identity-server-demo-api",
                DisplayName = "Identity Server Demo API",
                Scopes = new List<string>
                {
                    "write",
                    "read"
                }
            }
        };
        public static IEnumerable<ApiScope> ApiScopes = new List<ApiScope>
        {
            new ApiScope("openid"),
            new ApiScope("profile"),
            new ApiScope("email"),
            new ApiScope("read"),
            new ApiScope("write"),
            new ApiScope("identity-server-demo-api")
        };
    }
}
