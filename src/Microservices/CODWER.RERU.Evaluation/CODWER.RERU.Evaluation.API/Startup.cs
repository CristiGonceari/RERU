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
using System.Text;
using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation.AspNetCore;
using MediatR;
using System;
using AutoMapper.EquivalencyExpression;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.Data.Persistence.Initializer;
using Wkhtmltopdf.NetCore;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using ServicesSetup = CODWER.RERU.Evaluation.API.Config.ServicesSetup;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;

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
            services.Configure<KestrelServerOptions>(options =>
                {
                    options.AllowSynchronousIO = true;
                    //options.Limits.MaxRequestBodySize = null; --did not worked
                    options.Limits.MaxRequestBodySize = int.MaxValue;
                })
                // If using IIS:
                .Configure<IISServerOptions>(options =>
                {
                    options.AllowSynchronousIO = true;
                    //options.MaxRequestBodySize = null;
                    options.MaxRequestBodySize = int.MaxValue;
                })
                .Configure<FormOptions>(options =>
                {
                    options.ValueLengthLimit = int.MaxValue;
                    options.MultipartBodyLengthLimit = int.MaxValue; // if don't set default value is: 128 MB
                    options.MultipartHeadersLengthLimit = int.MaxValue;
                });

            services.Configure<SmtpOptions>(this.Configuration.GetSection("Smtp"));
            services.Configure<RabbitMq>(Configuration.GetSection("MessageQueue"));
            services.Configure<MinioSettings>(this.Configuration.GetSection("Minio"));


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
            services.AddCommonModuleApplication();
            services.AddModuleApplicationServices();

            services.AddHangfire(config =>
                config.UseSqlServerStorage(Configuration.GetConnectionString("Default")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            DatabaseSeeder.SeedDb(appDbContext);

            //app.UseHangfireDashboard();
            //app.UseHangfireServer();

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
