using CODWER.RERU.Core.Application.Common.ExceptionHandlers.Response;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Application.Common.Services.Identity.IdentityServer;
using CODWER.RERU.Core.Application.Common.Services.Password;
using CODWER.RERU.Core.Application.Module.Providers;
using CVU.ERP.Module.Application.DependencyInjection;
using CVU.ERP.Module.Application.Providers;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.Module.Common.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Core.Application.DependencyInjection
{
    public static class CoreApplicationServicesSetup
    {
        public static IServiceCollection AddCoreModuleApplication(this IServiceCollection services)
        {
            // Use ERP.Module.Application
            services.AddCommonModuleApplication();

            //Module dependencies
            services.AddTransient<IApplicationUserProvider, CoreApplicationUserProvider>();
            services.AddTransient<IModulePermissionProvider, CoreModulePermissionProvider>();
            // end of module dependencies

            services.AddTransient<ICommonServiceProvider, CommonServiceProvider>();
            services.AddTransient<IIdentityService, IdentityServerIdentityService>();
            services.AddTransient<IPasswordGenerator, PasswordGenerator>();

            //Exception handlers
            services.AddTransient<IResponseExceptionHandler, CreateIdentityFailedExceptionHandler>();

            //end exception handlers
            return services;
        }
    }
}