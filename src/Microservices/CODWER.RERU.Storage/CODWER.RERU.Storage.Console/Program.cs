using CVU.ERP.StorageService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;

namespace CODWER.RERU.Storage.Console
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            Migrate(host.Services);

            return Task.FromResult(0);
        }

        static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((hostingContext, services) => services.AddDbContext<StorageDbContext>(options =>
                options.UseNpgsql(hostingContext.Configuration.GetConnectionString(ConnectionString.Storage),
                    b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))));

        static void Migrate(IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var dbContext = provider.GetService<StorageDbContext>();
            dbContext.Database.Migrate();

            System.Console.WriteLine("Latest migrations applied");
        }
    }
}
