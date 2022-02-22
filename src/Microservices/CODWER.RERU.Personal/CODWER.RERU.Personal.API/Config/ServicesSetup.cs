using CODWER.RERU.Personal.Application;
using CODWER.RERU.Personal.Application.DependencyInjection;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Services.Implementations;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Infrastructure;
using CVU.ERP.Infrastructure.Email;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ISession = CODWER.RERU.Personal.Application.Interfaces.ISession;

namespace CODWER.RERU.Personal.API.Config
{
    public static class ServicesSetup
    {
        #region Entity

        public static void ConfigureEntity(IServiceCollection services, IConfiguration configuration)
        {
            // Add framework services.
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default"),
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

            services.AddTransient<IContractorCodeGeneratorService, ContractorCodeGeneratorService>();

            services.AddScoped<ISession, Session>();

            services.AddPersonalApplication(currentEnvironment);
        }

        #endregion
    }
}
