
using Film_API.Data;
using Film_API.Services.Movies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Film_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Film API",
                    Description = "API for managing a Film database",
                    TermsOfService = new Uri("https://google.com/search?q=the+best+terms+of+service"),
                    Contact = new OpenApiContact
                    {
                        Name = "Michael Amundsen",
                        Url = new Uri("https://google.com/search?q=michael+amundsen")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Made-up License",
                        Url = new Uri("https://google.com/search?q=made-up+license")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
            builder.Services.AddDbContext<FilmDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Film")));
            builder.Services.AddScoped<IMovieService, MovieService>();

            var app = builder.Build();

            // if (app.Environment.IsDevelopment())
            // {
                app.UseSwagger();
                app.UseSwaggerUI();
            // }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}