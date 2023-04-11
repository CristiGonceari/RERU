using AutoMapper.EquivalencyExpression;
using CODWER.RERU.Logging.Application.DependencyInjection;
using CODWER.RERU.Logging.Application.Validations;
using CODWER.RERU.Logging.Persistence;
using CODWER.RERU.Logging.Persistence.DbSeeder;
using Newtonsoft.Json.Serialization;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Logging.Context;
using CVU.ERP.Module;
using CVU.ERP.Module.Application.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using CVU.ERP.Common;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using RERU.Data.Persistence.Context;
using Wkhtmltopdf.NetCore;
using Age.Integrations.MSign.Soap;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Logging.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString(ConnectionString.Common),
                    b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddDbContext<LoggingDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString(ConnectionString.Logging),
                            b => b.MigrationsAssembly(typeof(DatabaseSeeder).GetTypeInfo().Assembly.GetName().Name)));

            services.AddTransient<IPaginationService, PaginationService>();

            services.AddCors();

            services.AddMediatR(typeof(ValidationService).Assembly);
            services.AddAutoMapper(cfg => { cfg.AddCollectionMappers(); }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddOptions();
            services.AddMemoryCache();

            services.AddControllers()
                .AddERPModuleControllers();
            services.AddWkhtmltopdf();

            services.AddLoggingApplication();

            services.AddERPModuleServices(Configuration);
            //services.AddCommonModuleApplication(Configuration);
            //services.AddModuleApplicationServices();
            services.AddLoggingSetup(Configuration);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CODWER.RERU.Logging.API", Version = "v1" });
            });

            services.AddTransient<IDateTime, MachineDateTime>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, LoggingDbContext appDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CODWER.RERU.Logging.API v1"));
            }

            app.UseAuthentication();

            DatabaseSeeder.Migrate(appDbContext);

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors(
                    options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                );
            }


            app.UseAuthorization();
            app.UseERPMiddlewares();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("api/MSignRedirect/{msignRequestId}", async (string msignRequestId, string returnUrl, [FromServices] IMSignSoapClient msignClient) =>
                {
                    var msignRedirectUrl = msignClient.BuildRedirectAddress(msignRequestId, "MSignService/MSignCallback/" + msignRequestId) + $"?redirectUrl={returnUrl}"; ;
                    return Results.Redirect(msignRedirectUrl);
                });
            });
        }
    }
}
