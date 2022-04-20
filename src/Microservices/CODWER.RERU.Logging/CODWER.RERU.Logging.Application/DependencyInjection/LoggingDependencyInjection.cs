using CODWER.RERU.Logging.Application.Permissions;
using CVU.ERP.Module.Common.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Logging.Application.DependencyInjection
{
    public static class LoggingDependencyInjection
    {
        public static IServiceCollection AddLoggingApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IModulePermissionProvider), typeof(ModulePermissionProvider));

            return services;
        }
    }
}
