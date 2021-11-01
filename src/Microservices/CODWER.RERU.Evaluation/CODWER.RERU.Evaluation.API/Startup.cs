using CVU.ERP.Infrastructure.Email;
using CVU.ERP.MessageQueue;
using CVU.ERP.Module;
using CVU.ERP.Module.Application.DependencyInjection;
using Hangfire;
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
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Http;
using ServicesSetup = CODWER.RERU.Evaluation.API.Config.ServicesSetup;

namespace CODWER.RERU.Evaluation.API
{
    public class Startup
    {
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
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<ITestService, TestService>();

            services.Configure<SmtpOptions>(this.Configuration.GetSection("Smtp"));
            services.Configure<RabbitMq>(Configuration.GetSection("MessageQueue"));

            ServicesSetup.ConfigureEntity(services, Configuration);
            ServicesSetup.ConfigurePolicies(services);
            ServicesSetup.ConfigureInjection(services);

            // Add services
            services.AddCors();
            services.AddOptions();
            services.AddMemoryCache();

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            services.AddOptions();
            services.AddMemoryCache();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

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

            services.AddERPModuleServices(Configuration);
            services.AddCommonModuleApplication();
            services.AddModuleApplicationServices();

            services.AddHangfire(config =>
                config.UseSqlServerStorage(Configuration.GetConnectionString("Default")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseHangfireDashboard();
            app.UseHangfireServer();

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
