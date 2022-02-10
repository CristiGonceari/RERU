using CVU.ERP.StorageService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CVU.ERP.StorageService.DependencyInjection
{
    public static class StorageServiceSetup
    {
        public static IServiceCollection AddCommonStorageContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<StorageDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Storage"),
                    b => b.MigrationsAssembly(typeof(StorageDbContext).GetTypeInfo().Assembly.GetName().Name)));

            return services;
        }
    }
}
