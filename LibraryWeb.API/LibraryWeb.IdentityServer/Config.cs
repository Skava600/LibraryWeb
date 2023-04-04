using IdentityModel;
using IdentityServer4.Models;

namespace LibraryWeb.IdentityServer
{
    public static class Config
    {


        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("LibraryWeb.Api", "Web Api", new [] { JwtClaimTypes.Name })
            {
                Scopes = { "LibraryWeb.Api" }
            }

        };
        public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("LibraryWeb.Api", "Library web api")
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "demo_api_swagger",
                ClientName = "Swagger UI for demo_api",
                ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                RedirectUris = {"https://localhost:5001/swagger/oauth2-redirect.html"},
                AllowedCorsOrigins = {"https://localhost:5001"},
                AllowedScopes = {"LibraryWeb.Api"}
            }
        };
    }
}
