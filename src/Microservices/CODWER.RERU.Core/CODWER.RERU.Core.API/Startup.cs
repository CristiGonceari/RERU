using CODWER.RERU.Core.API.Config;
using CODWER.RERU.Core.Application.DependencyInjection;
using CODWER.RERU.Core.Application.Modules.UpdateSelfAsModule;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.DataTransferObjects.Me;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Identity.Models;
using CVU.ERP.Infrastructure.Email;
using CVU.ERP.Logging.DependencyInjection;
using CVU.ERP.Module;
using MediatR;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using RERU.Data.Persistence.Context;
using System.Text;
using CODWER.RERU.Core.Application.CronJobs;
using CVU.ERP.Common.DataTransferObjects.Config;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Module.Application.DependencyInjection;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Initializer;
using Wkhtmltopdf.NetCore;
using ServicesSetup = CODWER.RERU.Core.API.Config.ServicesSetup;



namespace CODWER.RERU.Core.API
{
    public class Startup
    {
        private const string CustomJsonMediaType = "application/vnd.cvu.hateoas+json";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;

            _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecurityKey"]));
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        private readonly SymmetricSecurityKey _securityKey;

        // This method gets called by the runtime. Use this method to add services to the container.
        //[Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));
            // services.Configure<RabbitMq> (Configuration.GetSection ("MessageQueue"));
            //services.Configure<ModuleConfiguration> (Configuration.GetSection ("ERPModule"));
            services.Configure<PlatformConfig>(Configuration.GetSection("PlatformConfig"));
            services.Configure<TenantDto>(Configuration.GetSection("CoreSettings").GetSection("Tenant"));
            services.Configure<ActiveTimeDto>(Configuration.GetSection("CoreSettings").GetSection("ActiveTime"));

            services.AddCoreServiceProvider(); // before conf AppDbContext

            ServicesSetup.ConfigureEntity(services, Configuration);
            ServicesSetup.ConfigureInjection(services);

            // Add Identity services to the services container.
            services.AddIdentity<ERPIdentityUser, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireDigit = false;

                opts.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ĂăÎîȘșȚțÂâ";
                opts.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<UserManagementDbContext>()
                .AddUserManager<UserManager<ERPIdentityUser>>()
                .AddDefaultTokenProviders();

            // Add services
            services.AddCors();

            services.AddOptions();
            services.AddMemoryCache();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerDocument(document =>
            {
                // Add an authenticate button to Swagger for JWT tokens
                document.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT"));
                document.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT", new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}. You can get a JWT token from /Authorization/Authenticate.",
                }));

                // Post process the generated document
                document.PostProcess = d => d.Info.Title = "CODWER.RERU.Core REST Client!";
            });

            services.AddControllers()
                .AddERPModuleControllers();
            services.AddWkhtmltopdf();



            services.AddERPModuleServices(Configuration) 
                .AddCoreModuleApplication(Configuration)
                .AddCommonLoggingContext(Configuration);

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString(ConnectionString.HangfireCore)));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext appDbContext, IMediator mediator, HangfireDbContext hangfireDbContext)
        {

            //app.UseApiResponseAndExceptionWrapper ()

            app.UseAuthentication();

            app.UseSwaggerUi3(settings =>
            {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            hangfireDbContext.Database.Migrate();
            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<SendEmailJob>(x => x.SendEmailNotification(), "*/1 * * * *");

            app.UseRouting();
            // global cors policy
            if (env.IsDevelopment())
            {
                // app.UseCors (x => x
                //     .WithOrigins ("http://localhost:4200")
                //     .AllowAnyMethod ()
                //     .AllowAnyHeader ()
                //     .AllowCredentials ());
                //   app.UseCors(CorsOptions.AllowAll);
                app.UseCors(
                    options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                );
            }

            //DatabaseSeeder.SeedDb(appDbContext);

            app.UseERPMiddlewares();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseOpenApi();

            app.UseSwaggerUi3();

            mediator.Send(new UpdateSelfAsModuleCommand()).Wait();
        }
    }

    // public static class Extensions
    // {
    //     public static IServiceCollection AddERPModuleServicesCore(this IServiceCollection services, IConfiguration configuration)
    //     {
    //         services.Configure<ModuleConfiguration>(configuration.GetSection("Module"));
    //         var sp = services.BuildServiceProvider();
    //         var erpConfiguration = sp.GetService<IOptions<ModuleConfiguration>>()?.Value;

    //         if (erpConfiguration != null && erpConfiguration.UsesAuthentication)
    //         {
    //             services.AddAuthentication(config =>
    //             {
    //                 config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //                 config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //             })
    //             // .AddJwtBearer("Bearer", options =>
    //             // {
    //             //  //   options.Authority = erpConfiguration.Authentication.Authority;
    //             //     options.Authority = "http://identity-new:8080/ms/identity-new";
    //             //     options.RequireHttpsMetadata = false;

    //             //     options.TokenValidationParameters = new TokenValidationParameters
    //             //     {
    //             //         ValidateAudience = false
    //             //     };

    //             // });
    //                 .AddIdentityServerAuthentication("Bearer", config =>
    //                 {
    //                    // config.Authority = erpConfiguration.Authentication.Authority;
    //                     config.Authority = "http://identity-new:8080/ms/identity-new/";

    //                     config.RequireHttpsMetadata = erpConfiguration.Authentication.RequireHttpsMetadata;
    //                     // config.TokenValidationParameters = new TokenValidationParameters
    //                     // {
    //                     //     ValidateAudience = false
    //                     // };
    //                 });
    //         }
    //         return services;
    //     }
    // }
}