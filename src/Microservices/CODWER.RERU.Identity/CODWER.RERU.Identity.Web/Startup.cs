// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Reflection;
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
using Age.Integrations.MPass.Saml;
using RERU.Data.Persistence.Context;
using AutoMapper.EquivalencyExpression;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Services.Implementations;
using CVU.ERP.Common;
using CVU.ERP.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

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
            services.Configure<MPassSamlOptions>(MPassSamlDefaults.AuthenticationScheme, Configuration.GetSection("MPassSaml"));
            services.AddSystemCertificate(Configuration.GetSection("Certificate"));

            services.AddAutoMapper(cfg => { cfg.AddCollectionMappers(); }, AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, MachineDateTime>();

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

            services.AddDbContext<AppDbContext>(options =>
                        options.UseNpgsql(Configuration.GetConnectionString(ConnectionString.Common),
                            b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name)));

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

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = MPassSamlDefaults.AuthenticationScheme;
            })
                .AddCookie(options =>
                {
                    options.LogoutPath = "/Account/Logout";
                    //Setting to None to allow POST from MPass (default is Lax)
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                })
                .AddMPassSaml(Configuration.GetSection("MPassSaml"), options => 
                {
                    options.InvalidResponseRedirectUri = "/External/CancelMPass" ;
                    options.SignedOutRedirectUri = "/Account/Logout";
                });
                //.AddSaml2(options =>
                //{
                //    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //    options.SignOutScheme = IdentityServerConstants.DefaultCookieAuthenticationScheme;
                //    // options.SPOptions.ReturnUrl = new Uri("http://localhost:5000");
                //    options.SPOptions.EntityId = new EntityId(Configuration.GetValue<string>("SAML_ENTITY_ID"));
                //    options.IdentityProviders.Add(
                //        new IdentityProvider(
                //            new EntityId(Configuration.GetValue<string>("SAML_METADATA_ENDPOINT")), options.SPOptions)
                //        {
                //            LoadMetadata = true
                //        });
                //    //options.SPOptions.PublicOrigin = new Uri("ms/identity-new/saml2");
                //    options.SPOptions.ServiceCertificates.Add(new X509Certificate2("erp_platform.pfx", "7Q[CM8fbv(!^JdJD"));
                //    options.SPOptions.ValidateCertificates = false;
                //    options.SPOptions.MinIncomingSigningAlgorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
                //});
           
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

                    endpoints.MapGet("/mpass-slo", async context =>
                    {
                        var idToken = await context.GetTokenAsync("id_token");
                        var redirectUrl = "/connect/endsession?id_token_hint=" + idToken;

                        context.Response.Redirect(redirectUrl);
                    });

                    endpoints.MapGet("/get-token-id", async context =>
                    {
                        // Retrieve the token_id from the request
                        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
                        var token_id = string.Empty;
                        if (!string.IsNullOrEmpty(token))
                        {
                            var jwtHandler = new JwtSecurityTokenHandler();
                            var jwtToken = jwtHandler.ReadJwtToken(token);
                            token_id = jwtToken.Id;
                        }

                        // Further processing or return the token_id as needed
                        await context.Response.WriteAsync($"Token ID: {token_id}");
                    });

                });

                app.UseIdentityServer();
            });
        }
    }
}