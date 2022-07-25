using System.Reflection;
using CVU.ERP.Common;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.API.Config {
    public static class ServicesSetup {
        #region Entity
        public static void ConfigureEntity (
            IServiceCollection services,
            IConfiguration configuration
        ) {
            //Add framework services.
            //services
            //    .AddDbContext<AppDbContext> (options =>
            //        options.UseNpgsql(configuration.GetConnectionString ("Default"),
            //            b => b.MigrationsAssembly (typeof (AppDbContext).GetTypeInfo ().Assembly.GetName ().FirstName)));

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionString.Common),
                    b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddDbContext<HangfireDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionString.HangfireCore),
                    b => b.MigrationsAssembly(typeof(HangfireDbContext).GetTypeInfo().Assembly.GetName().Name)));

            services
                .AddDbContext<UserManagementDbContext> (options =>
                    options.UseNpgsql(configuration.GetConnectionString (ConnectionString.Identity),
                        b => b.MigrationsAssembly (typeof (UserManagementDbContext).GetTypeInfo ().Assembly.GetName ().Name)));

        }
        #endregion

        #region DI
        public static void ConfigureInjection (IServiceCollection services) {
            services.AddTransient<IPaginationService, PaginationService> ();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor> ();
            services.AddTransient<IDateTime, MachineDateTime> ();
        }
        #endregion
    }
}