using CVU.ERP.Logging;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.ExceptionHandlers;
using CVU.ERP.Module.Application.Infrastructure;
using CVU.ERP.Module.Application.LoggerServices.Implementations;
using CVU.ERP.Module.Application.Providers;
using CVU.ERP.Module.Application.StorageFileServices.Implementations;
using CVU.ERP.Module.Application.TablePrinterService;
using CVU.ERP.Module.Application.TablePrinterService.Implementations;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.Notifications.DependencyInjection;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using src.ExceptionHandlers;
using System;
using System.Reflection;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Infrastructure.Email;
using CVU.ERP.Logging.Context;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.DependencyInjection;
using CVU.ERP.StorageService.Models;

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
            services.AddTransient(typeof(IStorageFileService), typeof(StorageFileService));
            services.Configure<MinioSettings>(configuration.GetSection("Minio"));
            services.AddCommonStorageContext(configuration);

            //log service 
            services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));

            //print table service
            services.AddTransient(typeof(ITablePrinter<,>), typeof(TablePrinter<,>));

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
            services.AddTransient(typeof(ITablePrinter<,>), typeof(TablePrinter<,>));

            return services;
        }
    }
}