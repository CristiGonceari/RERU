using System;
using System.Reflection;
using AutoMapper.EquivalencyExpression;
using CODWER.RERU.Logging.Application.DependencyInjection;
using CODWER.RERU.Logging.Application.Validations;
using CODWER.RERU.Logging.Persistence;
using CODWER.RERU.Logging.Persistence.DbSeeder;
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
            services.AddDbContext<LoggingDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Log"),
                            b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddCors();

            services.AddMediatR(typeof(ValidationService).Assembly);
            services.AddAutoMapper(cfg => { cfg.AddCollectionMappers(); }, AppDomain.CurrentDomain.GetAssemblies());

            services.AddOptions();
            services.AddMemoryCache();

            services.AddControllers()
                .AddERPModuleControllers();

            services.AddLoggingApplication();

            services.AddERPModuleServices(Configuration);
            services.AddCommonModuleApplication();
            services.AddModuleApplicationServices();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CODWER.RERU.Logging.API", Version = "v1" });
            });

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

            app.UseAuthorization();
            app.UseERPMiddlewares();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
