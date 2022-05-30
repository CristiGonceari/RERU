using System;
using CVU.ERP.Module.API.DependencyInjection;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Kubernetes;
using Ocelot.Provider.Polly;

namespace CODWER.RERU.Gateway.Public
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

            services.AddControllers();

            var authenticationProviderKey = "Testkey";

            Action<IdentityServerAuthenticationOptions> opt = o =>
            {
                o.Authority = "http://localhost:44339";
                o.ApiName = "SampleService";
                o.SupportedTokens = SupportedTokens.Both;
            };

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(authenticationProviderKey, opt);

            services.AddControllers().AddCommonModuleControllers();

            services.AddOcelot(Configuration)
                .AddPolly()
                .AddKubernetes();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseWebSockets();
            app.UseOcelot().Wait();
        }
    }
}