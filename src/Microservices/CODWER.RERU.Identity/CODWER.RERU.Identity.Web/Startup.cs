// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using CODWER.RERU.Identity.Web.Quickstart.Models;
using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Identity.Context;
using CVU.ERP.Identity.Models;
using IdentityServer4;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sustainsys.Saml2;
using Sustainsys.Saml2.Metadata;

namespace CODWER.RERU.Identity.Web
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<PlatformConfig>(Configuration.GetSection("PlatformConfig"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.OnAppendCookie = cookieContext => CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext => CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                //this is important. this will help you to solve the problem.
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddControllersWithViews();

            services.AddDbContext<IdentityDbContext>(options =>
                        options.UseNpgsql(Configuration.GetConnectionString(ConnectionString.Identity),
                            b => b.MigrationsAssembly(typeof(IdentityDbContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddIdentity<ERPIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddUserManager<UserManager<ERPIdentityUser>>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.IssuerUri = Configuration.GetValue<string>("IDENTITY_ISSUER_URI");
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
                options.Cors.CorsPaths.Add(Configuration.GetValue<string>("SAML_CORS_PATH"));
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Configuration.GetSection("IdentityServer:ApiScopes"))
                .AddInMemoryClients(Configuration.GetSection("IdentityServer:Clients"))
                .AddAspNetIdentity<ERPIdentityUser>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddAuthentication()
                            .AddSaml2(options =>
                            {
                                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                                options.SignOutScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
                                // options.SPOptions.ReturnUrl = new Uri("http://localhost:5000");
                                options.SPOptions.EntityId = new EntityId(Configuration.GetValue<string>("SAML_ENTITY_ID"));
                                options.IdentityProviders.Add(
                                    new IdentityProvider(
                                        new EntityId(Configuration.GetValue<string>("SAML_METADATA_ENDPOINT")), options.SPOptions)
                                    {
                                        LoadMetadata = true
                                    });
                                //options.SPOptions.PublicOrigin = new Uri("ms/identity-new/saml2");
                                options.SPOptions.ServiceCertificates.Add(new X509Certificate2("erp_platform.pfx", "7Q[CM8fbv(!^JdJD"));
                                options.SPOptions.ValidateCertificates = false;
                                options.SPOptions.MinIncomingSigningAlgorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
                            });
            
            services.AddCors();
            services.AddSwaggerGen();
        }

        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None || !httpContext.Request.IsHttps)
            {
                // Unspecified - no SameSite will be included in the Set-Cookie.
                options.SameSite = (SameSiteMode)(-1);
                // options.Secure = true;
            }
        }

        public void Configure(IApplicationBuilder app, System.IServiceProvider serviceProvider, IdentityDbContext identityDbContext)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI();

            app.Use(async (ctx, next) =>
            {
                if (ctx.Request.Headers.ContainsKey("EXTERNAL_PROXIED"))
                {

                    ctx.SetIdentityServerOrigin(Configuration.GetValue<string>("IDENTITY_DISCOVERY_ENDPOINT_URI"));
                }
                await next();
            });
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //  app.UseDatabaseErrorPage();
            }

            identityDbContext.Database.Migrate();
            DbSeeder.SeedDb(serviceProvider);

            var basePath = Configuration.GetValue<string>("BASE_PATH") ?? string.Empty;
            app.UseCookiePolicy();

            app.Map(new PathString(basePath), app =>
            {


                app.UseCors(b => b
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(new[] { "http://localhost:4200", "http://localhost:4201", "http://localhost:4202" })
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition"));
                // uncomment if you want to add MVC
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                var managementPort = Configuration.GetValue<int>("Application:ManagementPort");

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                });

                app.UseIdentityServer();
            });
        }
    }
}