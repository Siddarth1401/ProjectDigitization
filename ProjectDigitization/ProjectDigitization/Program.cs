using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ProjectDigitization.DataAccess.Context;
using ProjectDigitization.DataAccess.Repositories;
using ProjectDigitization.Interfaces.Repository;
using ProjectDigitization.Interfaces.Services;
using ProjectDigitization.Services.Services;
using Serilog.Extensions.Logging;
using Serilog;
using ProjectDigitization.Interfaces.Logging;
using ProjectDigitization.Services.Logging;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuration & services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo { Title = "ProjectDigitization API", Version = "v1" });
        });

        // EF Core DbContext (ensure DefaultConnection exists in appsettings.json)
        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // DI registrations for repository & service
        builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
        builder.Services.AddScoped<IProductServices, ProductServices>();

        //Logger block
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();
        builder.Host.UseSerilog();
        builder.Services.AddScoped(typeof(IGenericLogger<>), typeof(GenericLogger<>));


        var app = builder.Build();

        // Swagger UI (development only)
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            { 
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectDigitization API v1");
                c.RoutePrefix = string.Empty;
            });  
        }

        app.UseHttpsRedirection();
        app.MapControllers();

        await app.RunAsync();
    }
}