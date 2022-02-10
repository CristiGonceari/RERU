using CVU.ERP.Module.API.DependencyInjection;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper;
using CVU.ERP.Module.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CVU.ERP.Module {
    public static class ERPModuleDependencyInjection {

        public static IMvcBuilder AddERPModuleControllers (this IMvcBuilder builder) 
        {
            builder.AddCommonModuleControllers ();
            return builder;
        }

        public static IServiceCollection AddERPModuleServices (this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<ModuleConfiguration> (configuration.GetSection ("Module"));
           
            var sp = services.BuildServiceProvider ();
            var erpConfiguration = sp.GetService<IOptions<ModuleConfiguration>> ()?.Value;

            if (erpConfiguration != null && erpConfiguration.UsesAuthentication) {
                services.AddAuthentication (x => {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddIdentityServerAuthentication ("Bearer", config => {
                        config.Authority = erpConfiguration.Authentication.Authority;
                        config.RequireHttpsMetadata = erpConfiguration.Authentication.RequireHttpsMetadata;
                    });
            }

            return services;
        }

        public static IApplicationBuilder UseERPMiddlewares (this IApplicationBuilder builder) 
        {
            builder.UseMiddleware<ResponseWrapperMiddleware> (new ResponseWrapperOptions ());
            return builder;
        }
    }
}