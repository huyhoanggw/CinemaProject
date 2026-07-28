
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Cinema.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cinema API",
                    Version = "v1"
                });
                options.AddSecurityDefinition("oidc", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5004/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5004/connect/token"),
                            Scopes = new Dictionary<string, string>
                            {
                                         { "openid", "OpenID" },
                                           { "profile", "Profile" },
                                       { "cinema.read", "Cinema Read" },
                                       { "cinema.write", "Cinema Write" }
                            }
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement //gắn các jwt , scope vào request
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id= "oidc",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new[] {"cinema.read" , "cinema.write","profile","openid"}
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.OAuthClientId("cinema-swagger");
                    options.OAuthUsePkce();
                }
                
                    );
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
