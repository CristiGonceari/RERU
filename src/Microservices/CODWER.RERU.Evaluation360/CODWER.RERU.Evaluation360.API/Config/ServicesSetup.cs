using System.Reflection;
using CODWER.RERU.Evaluation360.Application;
using CODWER.RERU.Evaluation360.Application.DependencyInjection;
using CODWER.RERU.Evaluation360.Application.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using CVU.ERP.Common;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Infrastructure;
using Microsoft.AspNetCore.Http;
using RERU.Data.Persistence.Context;
using ISession = CODWER.RERU.Evaluation360.Application.Interfaces.ISession;
using CVU.ERP.OrganigramService.DependencyInjection;

namespace CODWER.RERU.Evaluation360.API.Config
{
    public static class ServicesSetup
    {
        #region Entity

        public static void ConfigureEntity(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionString.Common),
                    b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddDbContext<HangfireDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionString.HangfireEvaluation360),
                    b => b.MigrationsAssembly(typeof(HangfireDbContext).GetTypeInfo().Assembly.GetName().Name)));
        }
        #endregion

        #region Policy
        public static void ConfigurePolicies(IServiceCollection services)
        {
            // Example of access policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("IsAdmin", "true"));
            });
        }

        #endregion

        #region DI
        public static void ConfigureInjection(IServiceCollection services, IHostingEnvironment currentEnvironment)
        {
            services.AddTransient<IPaginationService, PaginationService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<ValidationService>();

            services.AddScoped<ISession, Session>();

            services.AddEvaluation360Application(currentEnvironment);

            //organigram-services
            services.AddOrganigramService();
        }

        #endregion
    }
}
