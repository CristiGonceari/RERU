using System.Reflection;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Logging.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.ERP.Logging.DependencyInjection
{
    public static class LogServicesSetup
    {
        public static IServiceCollection AddCommonLoggingContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<LoggingDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionString.Logging),
                    b => b.MigrationsAssembly(typeof(LoggingDbContext).GetTypeInfo().Assembly.GetName().Name)));

            return services;
        }
    }
}
