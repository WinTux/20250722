using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Universidad.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace Universidad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(s=>s.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver());
            builder.Services.AddDbContext<UniversidadDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ConexionMySQL"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ConexionMySQL"))));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IEstudianteRepository, EstudianteRepository>();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
