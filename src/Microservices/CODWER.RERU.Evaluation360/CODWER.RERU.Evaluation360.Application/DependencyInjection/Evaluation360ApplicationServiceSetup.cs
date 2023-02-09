using CODWER.RERU.Evaluation360.Application.BLL.Services;
using CODWER.RERU.Evaluation360.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Config;
using CVU.ERP.Module.Common.Providers;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Services.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Entities;

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
            
            services.AddTransient<INotificationService, NotificationService>()
                .AddTransient<IInternalNotificationService, InternalNotificationService>()
                .AddTransient<IPdfService, PdfService>()
                .AddTransient<IAssignRolesToArticle, AssignRolesToArticleService>();
        }
    }
}
