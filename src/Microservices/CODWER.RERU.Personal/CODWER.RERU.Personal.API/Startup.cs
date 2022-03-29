using AutoMapper.EquivalencyExpression;
using CODWER.RERU.Personal.Application.Security;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.Data.Persistence.Initializer;
using CODWER.RERU.Personal.DataTransferObjects.Employers;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Infrastructure.Email;
using CVU.ERP.Logging.DependencyInjection;
using CVU.ERP.MessageQueue;
using CVU.ERP.Module;
using CVU.ERP.Module.Application.DependencyInjection;
using FluentValidation.AspNetCore;
using Hangfire;
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
using CODWER.RERU.Personal.Application.CronJobs;
using Hangfire.PostgreSql;
using Wkhtmltopdf.NetCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using ServicesSetup = CODWER.RERU.Personal.API.Config.ServicesSetup;

namespace CODWER.RERU.Personal.API
{
    public class Startup
    {
        private const string CustomJsonMediaType = "application/vnd.cvu.hateoas+json";
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecurityKey"]));

            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        private readonly SymmetricSecurityKey _securityKey;
        private readonly IHostingEnvironment CurrentEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<SmtpOptions>(this.Configuration.GetSection("Smtp"));
            services.Configure<RabbitMq>(Configuration.GetSection("MessageQueue"));
            services.Configure<DocumentOptions>(Configuration.GetSection("DocumentOptions"));
            services.Configure<EmployerData>(Configuration.GetSection("EmployerData"));

            ServicesSetup.ConfigureEntity(services, Configuration);
            ServicesSetup.ConfigurePolicies(services);
            ServicesSetup.ConfigureInjection(services, CurrentEnvironment);

            var tokenConfigurationOptions = Configuration.GetSection(nameof(TokenProviderOptions));
            services.Configure<TokenProviderOptions>(options => {
                options.Issuer = tokenConfigurationOptions[nameof(TokenProviderOptions.Issuer)];
                options.Audience = tokenConfigurationOptions[nameof(TokenProviderOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
            });

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
                document.PostProcess = d => d.Info.Title = "CODWER.RERU.MP REST Client!";
            });

            services.AddControllers()
                .AddERPModuleControllers();
            services.AddWkhtmltopdf();

            services.AddERPModuleServices(Configuration);
            services.AddCommonModuleApplication(Configuration);
            services.AddModuleApplicationServices()
                .AddCommonLoggingContext(Configuration);

            //if (CurrentEnvironment.IsDevelopment())
            //{
           // services.AddTransient<ICurrentApplicationUserProvider, Application.Services.Implementations.MockCurrentApplicationUserProvider>();
            //{ 

            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("Default")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AppDbContext appDbContext)
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

            // if (env.IsDevelopment())
            // {
            DatabaseSeeder.SeedDb(appDbContext);
            // }

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<JobTimeSheetTable>(x => x.JobForNationalHolidays(), "*/2 * * * *");
            RecurringJob.AddOrUpdate<JobTimeSheetTable>(x => x.JobForWorkedHours(), "*/5 * * * *");

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
