
using OpenAI.Responses;

namespace Cinema.AI
{
    #pragma warning disable OPENAI001
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new InvalidOperationException("api key chua duoc tao ");
            var client = new ResponsesClient(apiKey);
            var reponse = await client.CreateResponseAsync("gpt-5.2",
    "Xin chào! Hãy giới thiệu bản thân bằng tiếng Việt.");
            Console.WriteLine(reponse.Value.ToString());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
