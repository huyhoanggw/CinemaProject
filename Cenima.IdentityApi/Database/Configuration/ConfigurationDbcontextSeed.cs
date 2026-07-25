using Cineima.IdentityApi.Configuration;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;

namespace Cinema.IdentityApi.Database.Configuration
{
    public class ConfigurationDbcontextSeed
    {
        public async Task SeedAsync(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var scope in Config.GetScopes())
                {
                    context.ApiScopes.Add(scope.ToEntity());
                }
                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var identity in Config.GetIdentities())
                {
                    context.IdentityResources.Add(identity.ToEntity());
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
