using CVU.ERP.Common.Interfaces;
using CVU.ERP.Infrastructure.Email;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.ERP.Notifications.DependencyInjection
{
    public static class NotificationServiceSetup
    {
        public static IServiceCollection AddNotificationService(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<INotificationService, NotificationService>();

            return services;
        }
    }
}
