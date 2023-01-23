using CVU.ERP.OrganigramService.Services;
using CVU.ERP.OrganigramService.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.ERP.OrganigramService.DependencyInjection
{
    public static class OrganigramServiceSetup
    {
        public static IServiceCollection AddOrganigramService(this IServiceCollection services)
        {
            services.AddTransient<IGetOrganigramService, GetOrganigramService>();

            return services;
        }
    }
}
