using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Identity.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.IdentityMigrator.Console
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
            .ConfigureServices((hostingContext, services) => services.AddDbContext<IdentityDbContext>(options =>
                options.UseNpgsql(hostingContext.Configuration.GetConnectionString(ConnectionString.Identity),
                        b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))))
            .ConfigureServices((hostingContext, services) => services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(hostingContext.Configuration.GetConnectionString(ConnectionString.Common),
                    b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName))));


        static void Migrate(IServiceProvider services)
        {
            using var serviceScope = services.CreateScope();
            var provider = serviceScope.ServiceProvider;

            var identityDbContext = provider.GetService<IdentityDbContext>();
            identityDbContext.Database.Migrate();

            var reruCommonContext = provider.GetService<AppDbContext>();
            reruCommonContext.Database.Migrate();

            System.Console.WriteLine("Latest migrations applied");
        }
    }
}
