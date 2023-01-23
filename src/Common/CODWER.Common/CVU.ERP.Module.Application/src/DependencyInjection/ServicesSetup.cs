using CVU.ERP.Common.DataTransferObjects.ConnectionStrings;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Infrastructure.Email;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Context;
using CVU.ERP.Module.Application.ExceptionHandlers;
using CVU.ERP.Module.Application.Infrastructure;
using CVU.ERP.Module.Application.LoggerService.Implementations;
using CVU.ERP.Module.Application.StorageFileServices.Implementations;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.Module.Application.TableExportServices.Implementations;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.Notifications.DependencyInjection;
using CVU.ERP.Notifications.Services;
using CVU.ERP.Notifications.Services.Implementations;
using CVU.ERP.ServiceProvider;
using CVU.ERP.ServiceProvider.Clients;
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
using CVU.ERP.Module.Application.ImportProcessServices;
using CVU.ERP.Module.Application.ImportProcessServices.Implementations;
using CVU.ERP.OrganigramService.DependencyInjection;

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
            
            //notification service
            services.AddNotificationService();

            //MNotify services
            services.AddMNotifyClient(configuration.GetSection("MNotify"));

            //storage service
            services.AddStorageService(configuration);

            //log service 
            services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));

            //export data table services 
            services.AddExportTableServices();

            //import process service
            services.AddTransient<IImportProcessService, ImportProcessService>();

            //organigram-services
            services.AddOrganigramService();


            return services;
        }

        public static IServiceCollection AddLoggingSetup(this IServiceCollection services, IConfiguration configuration)
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


            services.AddHttpContextAccessor();
            //BL

            var sp = services.BuildServiceProvider();
            var erpConfiguration = sp.GetService<IOptions<ModuleConfiguration>>()?.Value;

            //if use mock
            if (erpConfiguration != null && erpConfiguration.UseMockApplicationUser)
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

            //log service 
            services.AddTransient(typeof(ILoggerService<>), typeof(LoggerService<>));

            //export data table services 
            services.AddExportTableServices();

            //storage service
            services.AddStorageService(configuration);

            services.AddTransient<ICoreClient, CoreClient>();
            services.AddTransient<IApplicationUserProvider, ModuleApplicationUserProvider>();

            return services;

        }

        public static IServiceCollection AddModuleApplicationServices(this IServiceCollection services)
        {
            //check if not core
            services.AddTransient<ICoreClient, CoreClient>();
            services.AddTransient<IApplicationUserProvider, ModuleApplicationUserProvider>();

            return services;
        }

        public static IServiceCollection AddModuleServiceProvider(this IServiceCollection services)
        {
            services.AddServiceProvider();
            services.AddModuleApplicationServices();

            return services;
        }

        public static IServiceCollection AddCoreServiceProvider(this IServiceCollection services)
        {
            services.AddServiceProvider();

            return services;
        }

        private static IServiceCollection AddServiceProvider(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            //BL

            var sp = services.BuildServiceProvider();
            var erpConfiguration = sp.GetService<IOptions<ModuleConfiguration>>()?.Value;

            //if use mock
            if (erpConfiguration != null && erpConfiguration.UseMockApplicationUser)
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

            return services;
        }

        public static IServiceCollection ForAddMigration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IStorageFileService, StorageFileService>();

            services.AddDbContext<StorageDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionString.Storage),
                    b => b.MigrationsAssembly(typeof(StorageDbContext).GetTypeInfo().Assembly.GetName().Name)));

            services.AddTransient(typeof(IStorageFileService), typeof(StorageFileService));
            services.Configure<MinioSettings>(configuration.GetSection("Minio"));

            services.AddDbContext<LoggingDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString(ConnectionString.Logging),
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
                    options.Limits.MaxRequestBodySize = int.MaxValue;
                })
                .Configure<IISServerOptions>(options =>
                {
                    options.AllowSynchronousIO = true;
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