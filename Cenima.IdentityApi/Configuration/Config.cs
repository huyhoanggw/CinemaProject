using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Cineima.IdentityApi.Configuration
{
    public class Config
    {
        public static IEnumerable<Duende.IdentityServer.Models.IdentityResource> GetIdentities()
        {
            return new List<Duende.IdentityServer.Models.IdentityResource>
            {
                new Duende.IdentityServer.Models.IdentityResources.OpenId(),
                new Duende.IdentityServer.Models.IdentityResources.Profile(),
                new Duende.IdentityServer.Models.IdentityResources.Email(),
            };
        }
        public static IEnumerable<Duende.IdentityServer.Models.ApiScope> GetScopes()
        {
            return new List<Duende.IdentityServer.Models.ApiScope>
            {
                new Duende.IdentityServer.Models.ApiScope("cinema.read","Cinema Read"),
                new Duende.IdentityServer.Models.ApiScope("cinema.write","Cinema Write"),
                 };
        }

        public static IEnumerable<Duende.IdentityServer.Models.Client> GetClients()
        {
            return new List<Duende.IdentityServer.Models.Client> {
                // cenima web portal
                new Duende.IdentityServer.Models.Client
                {
                    ClientId ="cinema-web",
                    AllowedGrantTypes = GrantTypes.Code ,

    RedirectUris =
    {
        "https://localhost:5001/signin-oidc"
    },

    PostLogoutRedirectUris =
    {
        "https://localhost:5001/signout-callback-oidc"
    },
                    AllowedScopes =
                     {
                     "openid",
                     "profile",
                     "email",
                     "cinema.read"

                    }
                },
                // cenima web admin
                new Duende.IdentityServer.Models.Client
                {
                    ClientId = "cinema-web-admin" ,
                 AllowedGrantTypes = GrantTypes.Code ,

    RedirectUris =
    {
        "https://localhost:5002/signin-oidc"
    },

    PostLogoutRedirectUris =
    {
        "https://localhost:5002/signout-callback-oidc"
    },
                    AllowedScopes =
                     {
                     "openid",
                     "profile",
                     "email",
                      "cinema.read",
                      "cinema.write"
                     }
                },

                // cenima api 
                new Duende.IdentityServer.Models.Client
                { ClientId = "cinema-swagger",
    ClientName = "Cinema Swagger",

    AllowedGrantTypes = GrantTypes.Code,
    RequirePkce = true,
    RequireClientSecret = false,

    RedirectUris =
    {
        "https://localhost:5000/swagger/oauth2-redirect.html"
    },

    AllowedCorsOrigins =
    {
        "https://localhost:5000"
    },

    AllowedScopes =
    {
        IdentityServerConstants.StandardScopes.OpenId,
        IdentityServerConstants.StandardScopes.Profile,
        "cinema.read",
        "cinema.write"
    },

    AllowAccessTokensViaBrowser = true

                }


            };
        }
    }
}
