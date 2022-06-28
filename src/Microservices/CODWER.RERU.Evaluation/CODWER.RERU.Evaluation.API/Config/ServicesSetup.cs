using System.Reflection;
using CODWER.RERU.Evaluation.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using CODWER.RERU.Evaluation.Application.DependencyInjection;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Infrastructure;
using CVU.ERP.Infrastructure.Email;
using Microsoft.AspNetCore.Http;
using RERU.Data.Persistence.Context;
using ISession = CODWER.RERU.Evaluation.Application.Interfaces.ISession;

namespace CODWER.RERU.Evaluation.API.Config
{
    public static class ServicesSetup
    {
        #region Entity

        public static void ConfigureEntity(IServiceCollection services, IConfiguration configuration)
        {
            // Add framework services.
            //services.AddDbContext<AppDbContext>(options =>
            //   options.UseNpgsql(configuration.GetConnectionString("Default"),
            //   b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().FirstName)));

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionString.Common),
                    b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name)));
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

            services.AddEvaluationApplication(currentEnvironment);
        }

        #endregion
    }
}
