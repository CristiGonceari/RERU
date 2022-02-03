using CODWER.RERU.Core.Application.Common.ExceptionHandlers.Response;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Application.Common.Services.Identity.IdentityServer;
using CODWER.RERU.Core.Application.Common.Services.PasswordGenerator;
using CODWER.RERU.Core.Application.Module.Providers;
using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.DependencyInjection;
using CVU.ERP.Module.Application.Providers;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.Module.Common.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Core.Application.DependencyInjection
{
    public static class CoreApplicationServicesSetup
    {
        public static IServiceCollection AddCoreModuleApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Use ERP.Module.Application
            services.AddCommonModuleApplication(configuration);


            //Module dependencies
            services.AddTransient<IApplicationUserProvider, CoreApplicationUserProvider>();
            services.AddTransient<IModulePermissionProvider, ModulePermissionProvider>();
            // end of module dependencies

            services.AddTransient<ICommonServiceProvider, CommonServiceProvider>();
            services.AddTransient<IIdentityService, IdentityServerIdentityService>();
            services.AddTransient<IPasswordGenerator, PasswordGenerator>();

            //Exception handlers
            services.AddTransient<IResponseExceptionHandler, CreateIdentityFailedExceptionHandler>();

            //end exception handlers

            services.AddTransient<IEvaluationClient, EvaluationClient>();

            return services;
        }
    }
}