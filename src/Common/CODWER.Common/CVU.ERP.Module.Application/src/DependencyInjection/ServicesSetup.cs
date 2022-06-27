using CVU.ERP.Common.Interfaces;
using CVU.ERP.Infrastructure.Email;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Context;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.ExceptionHandlers;
using CVU.ERP.Module.Application.Infrastructure;
using CVU.ERP.Module.Application.Providers;
using CVU.ERP.Module.Application.StorageFileServices.Implementations;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.Module.Application.TableExportServices.Implementations;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.Notifications.DependencyInjection;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Services.Implementations;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.DependencyInjection;
using CVU.ERP.StorageService.Models;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using src.ExceptionHandlers;
using System;
using System.Reflection;
using CVU.ERP.Module.Application.LoggerService.Implementations;

namespace CVU.ERP.Module.Application.DependencyInjection
{
    public static class ServicesSetup
    {
        public static IServiceCollection AddCommonModuleApplication(this IServiceCollection services, IConfiguration configuration)
        {
            //Exception Handlers
            services.AddTransient<IResponseExceptionHandler, ApplicationRequestValidationResponseExceptionHandler>();
            services.AddTransient<IResponseExceptionHandler, ApplicationUnauthenticatedResponseExceptionHandler>();
            services.AddTransient<IResponseExceptionHandler, ApplicationUnauthorizedResponseExceptionHandler>();
            services.AddTransient<IResponseExceptionHandler, CoreClientResponseNotSuccessfulExceptionHandler>();

            //Mediator Pipeline behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestAuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            //Common API
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Automapper
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies()); //Mediator
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()); // FluentValidation
            //
            services.AddHttpContextAccessor();
            //BL

            var sp = services.BuildServiceProvider();
            var erpConfiguration = sp.GetService<IOptions<ModuleConfiguration>>()?.Value;

            //if use mock
            if (erpConfiguration is not null && erpConfiguration.UseMockApplicationUser)
            {
                services.AddTransient<ICurrentApplicationUserProvider, MockCurrentApplicationUserProvider>();
            }
            else
            {
                services.AddTransient<ICurrentApplicationUserProvider, CurrentApplicationUserProvider>();
            }
            //
            services.AddTransient<IRestClient, RestClient>();
            services.AddTransient<IModuleClient, ModuleClient>();
            services.AddNotificationService();

            //storage service
            services.AddStorageService(configuration);

            //log service 
            services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));

            //export data table services 
            services.AddExportTableServices();

            return services;
        }

        public static IServiceCollection AddModuleApplicationServices(this IServiceCollection services)
        {
            //check if not core
            services.AddTransient<ICoreClient, CoreClient>();
            services.AddTransient<IApplicationUserProvider, ModuleApplicationUserProvider>();

            return services;
        }

        public static IServiceCollection ForAddMigration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IStorageFileService, StorageFileService>();

            services.AddDbContext<StorageDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Storage"),
                    b => b.MigrationsAssembly(typeof(StorageDbContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddTransient(typeof(IStorageFileService), typeof(StorageFileService));
            services.Configure<MinioSettings>(configuration.GetSection("Minio"));

            services.AddDbContext<LoggingDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Log"),
                    b => b.MigrationsAssembly(typeof(LoggingDbContext).GetTypeInfo().Assembly.GetName().Name)));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));

            services.AddExportTableServices();
            services.AddExportCurrentUserService();


            return services;
        }

        private static IServiceCollection AddStorageService(this IServiceCollection services, IConfiguration configuration)
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

            services.AddTransient(typeof(IStorageFileService), typeof(StorageFileService));
            services.Configure<MinioSettings>(configuration.GetSection("Minio"));
            services.AddCommonStorageContext(configuration);

            return services;
        }

        private static IServiceCollection AddExportTableServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IExportData<,>), typeof(ExportData<,>));
            services.AddTransient(typeof(ITablePrinter<,>), typeof(TablePrinter<,>));
            services.AddTransient(typeof(ITableExcelExport<,>), typeof(TableExcelExport<,>));
            services.AddTransient(typeof(ITableXmlExport<,>), typeof(TableXmlExport<,>));

            return services;
        }

        private static IServiceCollection AddExportCurrentUserService(this IServiceCollection services)
        {
            
            services.AddTransient(typeof(ICurrentApplicationUserProvider), typeof(MockCurrentApplicationUserProvider));

            return services;
        }
    }
}