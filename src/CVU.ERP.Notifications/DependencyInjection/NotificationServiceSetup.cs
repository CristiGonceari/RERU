using CVU.ERP.Common.Interfaces;
using CVU.ERP.Infrastructure.Email;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.ERP.Notifications.DependencyInjection
{
    public static class NotificationServiceSetup
    {
        public static IServiceCollection ConfigureInjection(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}
