using System.Reflection;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.API.Config
{
    public static class ServicesSetup
    {
        #region Entity

        public static void ConfigureEntity(IServiceCollection services, IConfiguration configuration)
        {
            // Add framework services.
            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Default"),
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
        public static void ConfigureInjection(IServiceCollection services)
        {          
            //services.AddTransient<IPaginationService, PaginationService>();
            //services.AddTransient<IDateTime, MachineDateTime>();
            //services.AddTransient<IEmailService, EmailService>(); 
            //services.AddTransient<ValidationService>();            

            //services.AddTransient<IOptionService, OptionService>();
            //services.AddTransient<IQuestionUnitService, QuestionUnitService>();
            //services.AddTransient<INotificationService, NotificationService>();
            //services.AddTransient<IUserProfileProvider, UserProfileProvider>();
            //services.AddTransient<IModulePermissionProvider, ModulePermissionProvider>();

            //services.AddTransient<ICurrentApplicationUserProvider, MockDataCurrentApplicationUserProvider>();
        }

        #endregion
    }
}
