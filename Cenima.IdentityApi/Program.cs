
using Cenima.IdentityApi.Database;
using Cenima.IdentityApi.Database.Models;
using Cinema.IdentityApi.Database;
using Cinema.IdentityApi.Database.Configuration;
using Cinema.IdentityApi.Services;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Cinema.IdentityApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //add services 
            builder.Services.AddTransient<IProfileService, ProfileService>();
            // add dbcontext
            builder.Services.AddDbContext<ApplicationDbcontext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });
            // add identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbcontext>().AddDefaultTokenProviders();
            // add identity server
            builder.Services.AddIdentityServer(options =>
            {
                options.IssuerUri = "https://localhost:5004";
                options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
            }).AddAspNetIdentity<ApplicationUser>().AddConfigurationStore(
               options =>
               {
                   options.ConfigureDbContext = b =>
                   {
                       b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
                   };
               }).AddOperationalStore(options =>
               {
                   options.ConfigureDbContext = b =>
                   {
                       b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
                   };
               });
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();
                try
                {
                    // migrate applicationdbcontex
                    services.GetRequiredService<ApplicationDbcontext>()
                    .Database.Migrate();
                    // migrate ConfigurationDbcontext
                    services.GetRequiredService<ConfigurationDbContext>()
                        .Database.Migrate();
                    // migrate PersistedGrantDbcontext
                    services.GetRequiredService<PersistedGrantDbContext>()
                        .Database.Migrate();

                    await new ApplicationDbcontextSeed().SeedAsync(services.GetRequiredService<ApplicationDbcontext>(), services.GetRequiredService<ILogger<ApplicationDbcontextSeed>>());
                    await new ConfigurationDbcontextSeed().SeedAsync(services.GetRequiredService<ConfigurationDbContext>());
                    logger.LogWarning("seed data complete");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Database migration failed");
                }
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseAuthorization();


            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}
