using System.Reflection;
using CVU.ERP.Common;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.DependencyInjection;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Infrastructure;
using CVU.ERP.Infrastructure.Email;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Core.API.Config {
    public static class ServicesSetup {
        #region Entity
        public static void ConfigureEntity (
            IServiceCollection services,
            IConfiguration configuration
        ) {
            //Add framework services.
            services
                .AddDbContext<CoreDbContext> (options =>
                    options
                    .UseSqlServer (configuration
                        .GetConnectionString ("Default"),
                        b =>
                        b
                        .MigrationsAssembly (typeof (CoreDbContext)
                            .GetTypeInfo ()
                            .Assembly
                            .GetName ()
                            .Name)));

            services
                .AddDbContext<UserManagementDbContext> (options =>
                    options
                    .UseSqlServer (configuration
                        .GetConnectionString ("Identity"),
                        b =>
                        b
                        .MigrationsAssembly (typeof (UserManagementDbContext)
                            .GetTypeInfo ()
                            .Assembly
                            .GetName ()
                            .Name)));

        }
        #endregion

        #region DI
        public static void ConfigureInjection (IServiceCollection services) {
            services.AddTransient<IPaginationService, PaginationService> ();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor> ();
            services.AddTransient<IDateTime, MachineDateTime> ();
            services.AddTransient<IEmailService, EmailService> ();
            services.AddTransient<INotificationService, NotificationService> ();
        }
        #endregion
    }
}