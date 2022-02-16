using System.Reflection;
using CVU.ERP.Common;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Infrastructure;
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
                    options.UseNpgsql(configuration.GetConnectionString ("Default"),
                        b => b.MigrationsAssembly (typeof (CoreDbContext).GetTypeInfo ().Assembly.GetName ().Name)));

            services
                .AddDbContext<UserManagementDbContext> (options =>
                    options.UseNpgsql(configuration.GetConnectionString ("Identity"),
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