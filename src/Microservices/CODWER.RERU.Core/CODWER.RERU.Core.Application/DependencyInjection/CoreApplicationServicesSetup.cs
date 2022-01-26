using CODWER.RERU.Core.Application.Common.ExceptionHandlers.Response;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Common.Services.Identity;
using CODWER.RERU.Core.Application.Common.Services.Identity.IdentityServer;
using CODWER.RERU.Core.Application.Common.Services.PasswordGenerator;
using CODWER.RERU.Core.Application.Module.Providers;
using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.Application.Services;
using CODWER.RERU.Core.Application.Services.Implementations;
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
            services.AddTransient<IModulePermissionProvider, ModulePermissionProvider>();
            // end of module dependencies

            services.AddTransient<ICommonServiceProvider, CommonServiceProvider>();
            services.AddTransient<IIdentityService, IdentityServerIdentityService>();
            services.AddTransient<IPasswordGenerator, PasswordGenerator>();

            //Exception handlers
            services.AddTransient<IResponseExceptionHandler, CreateIdentityFailedExceptionHandler>();

            //end exception handlers

            services.AddTransient<IEvaluationUserProfileService, EvaluationUserProfileService>();

            return services;
        }
    }
}