
using Cinema.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Cinema.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
           {
               option.Authority = "https://localhost:5004";
               option.RequireHttpsMetadata = true;
               option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
               };
               option.Events = new JwtBearerEvents
               {
                   OnMessageReceived = context =>
                   {
                       Console.WriteLine("========== JWT RECEIVED ==========");
                       var auth = context.Request.Headers.Authorization.ToString();

                       Console.WriteLine($"Authorization: {auth}");
                       return Task.CompletedTask;
                   },
                   OnAuthenticationFailed = context =>
                   {
                       Console.WriteLine("========== JWT ERROR ==========");
                       Console.WriteLine(context.Exception);
                       Console.WriteLine("================================");

                       return Task.CompletedTask;
                   }


                                };
           }

           );
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
            builder.Services.AddAuthorization(options =>
            {
                var permissions = new[]
                {
        "food.create",
        "food.delete",
        "food.read",
        "food.update",

        "genre.create",
        "genre.delete",
        "genre.read",
        "genre.update",

        "movie.create",
        "movie.delete",
        "movie.read",
        "movie.update",

        "seat.create",
        "seat.delete",
        "seat.read",
        "seat.update",

        "showtime.create",
        "showtime.delete",
        "showtime.read",
        "showtime.update",

        "theater.create",
        "theater.delete",
        "theater.read",
        "theater.update"
                };

                foreach(var permission in permissions)
                {
                    options.AddPolicy(permission, policy =>
                    {
                        policy.RequireClaim("permission", permission);
                    });
                }
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
            app.UseAuthentication();
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
