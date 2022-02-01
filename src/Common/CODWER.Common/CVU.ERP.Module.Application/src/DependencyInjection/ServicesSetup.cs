using System;
using CVU.ERP.Logging;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.ExceptionHandlers;
using CVU.ERP.Module.Application.Infrastructure;
using CVU.ERP.Module.Application.LoggerServices.Implementations;
using CVU.ERP.Module.Application.Providers;
using CVU.ERP.Module.Application.TablePrinterService;
using CVU.ERP.Module.Application.TablePrinterService.Implementations;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.Notifications.DependencyInjection;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestSharp;
using src.ExceptionHandlers;

namespace CVU.ERP.Module.Application.DependencyInjection
{
    public static class ServicesSetup
    {
        public static IServiceCollection AddCommonModuleApplication(this IServiceCollection services)
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
    }
}