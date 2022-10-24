using CODWER.RERU.Evaluation360.Application.Models;
using CODWER.RERU.Evaluation360.Application.Permissions;
using CVU.ERP.Module.Common.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Evaluation360.Application.DependencyInjection
{
    public static class Evaluation360ApplicationServiceSetup
    {
        public static IServiceCollection AddEvaluation360Application(this IServiceCollection services, IHostingEnvironment currentEnvironment)
        {
            services.ConfigureValidatorServices(currentEnvironment);

            return services;
        }

        public static void ConfigureValidatorServices(this IServiceCollection services, IHostingEnvironment currentEnvironment)
        {
            services
                .AddScoped(typeof(IModulePermissionProvider), typeof(ModulePermissionProvider))
                .AddScoped(typeof(PlatformConfig));
        }
    }
}
