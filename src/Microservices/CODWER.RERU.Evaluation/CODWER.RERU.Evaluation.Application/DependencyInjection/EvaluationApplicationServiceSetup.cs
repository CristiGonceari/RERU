﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Services.GetPdfServices;
using CODWER.RERU.Evaluation.Application.Services.GetPdfServices.Implementations;
using CODWER.RERU.Evaluation.Application.Services.Implementations;
using CODWER.RERU.Evaluation.Application.Services.Implementations.Storage;
using CVU.ERP.Module.Common.Providers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Evaluation.Application.DependencyInjection
{
    public static class EvaluationApplicationServiceSetup
    {
        public static IServiceCollection AddEvaluationApplication(this IServiceCollection services, IHostingEnvironment currentEnvironment)
        {
            services.ConfigureValidatorServices(currentEnvironment);

            return services;
        }

        public static void ConfigureValidatorServices(this IServiceCollection services, IHostingEnvironment currentEnvironment)
        {
            services
                .AddScoped(typeof(IModulePermissionProvider), typeof(ModulePermissionProvider))
                .AddScoped(typeof(IQuestionUnitService), typeof(QuestionUnitService))
                .AddScoped(typeof(IUserProfileService), typeof(UserProfileService))
                .AddScoped(typeof(IStorageFileService), typeof(CloudStorageFileService))
                .AddScoped(typeof(IGetQuestionUnitPdf), typeof(GetQuestionUnitPdf))
                .AddScoped(typeof(IGetTestPdf), typeof(GetTestPdf))
                .AddScoped(typeof(IGetTestTemplatePdf), typeof(GetTestTemplatePdf))
                .AddScoped(typeof(IPdfService), typeof(PdfService))
                .AddScoped(typeof(IInternalNotificationService), typeof(InternalNotificationService))
                .AddScoped(typeof(ILoggingService<>), typeof(LoggingService<>));
        }
    }
}
