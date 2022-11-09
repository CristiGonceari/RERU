using AutoMapper.EquivalencyExpression;
using CODWER.RERU.Evaluation.Application.CronJobs;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Infrastructure.Email;
using CVU.ERP.Logging.DependencyInjection;
using CVU.ERP.MessageQueue;
using CVU.ERP.Module;
using CVU.ERP.Module.Application.DependencyInjection;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Text;
using RERU.Data.Persistence.Context;
using Wkhtmltopdf.NetCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using ServicesSetup = CODWER.RERU.Evaluation.API.Config.ServicesSetup;
using CVU.ERP.Common.DataTransferObjects.Config;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;

            _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecurityKey"]));
        }

        public IConfiguration Configuration { get; }

        private readonly SymmetricSecurityKey _securityKey;
        private readonly IHostingEnvironment CurrentEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmtpOptions>(this.Configuration.GetSection("Smtp"));
            services.Configure<RabbitMq>(Configuration.GetSection("MessageQueue"));
            services.Configure<PlatformConfig>(Configuration.GetSection("PlatformConfig"));
            if (CurrentEnvironment.IsDevelopment())
            {
                services.AddERPModuleServices(Configuration);
            }
            services.AddModuleServiceProvider(); // before conf AppDbContext

            ServicesSetup.ConfigureEntity(services, Configuration);
            ServicesSetup.ConfigurePolicies(services);
            ServicesSetup.ConfigureInjection(services, CurrentEnvironment);

            // Add services
            services.AddCors();

            services.AddMediatR(typeof(ValidationService).Assembly);
            services.AddAutoMapper(cfg => { cfg.AddCollectionMappers(); }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddOptions();
            services.AddMemoryCache();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ValidationService>())
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            // Customise default API behavour
            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerDocument(document => {
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
                document.PostProcess = d => d.Info.Title = "CODWER.RERU.Evaluation REST Client!";
            });

            services.AddControllers()
                .AddERPModuleControllers();
            services.AddWkhtmltopdf();

            services.AddERPModuleServices(Configuration); 
            services.AddCommonModuleApplication(Configuration);
            services.AddCommonLoggingContext(Configuration);

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString(ConnectionString.HangfireEvaluation)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HangfireDbContext hangfireDb)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            hangfireDb.Database.Migrate();

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<SendEmailNotificationBeforeTest>(x => x.SendNotificationBeforeTest(), "*/5 * * * *");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseCors(
                    options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                );
            }

            app.UseERPMiddlewares();

            app.UseSwaggerUi3(settings => {
                settings.Path = "/api";
                settings.DocumentPath = "/api/specification.json";
            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(routes => {
                routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();
        }
    }
}
