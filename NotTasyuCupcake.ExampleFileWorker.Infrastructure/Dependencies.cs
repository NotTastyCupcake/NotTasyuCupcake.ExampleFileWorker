using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotTasyuCupcake.ExampleFileWorker.Infrastructure.Data;
namespace NotTasyuCupcake.ExampleFileWorker.Infrastructure;
public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        bool useOnlyInMemoryDatabase = false;
        if (configuration["UseOnlyInMemoryDatabase"] != null)
        {
            useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]!);
        }

        if (useOnlyInMemoryDatabase)
        {
            services.AddDbContext<DataContext>(c =>
               c.UseInMemoryDatabase("Catalog"));
        }
        else
        {
            services.AddDbContext<DataContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("WordsConnection")));

        }
    }
}