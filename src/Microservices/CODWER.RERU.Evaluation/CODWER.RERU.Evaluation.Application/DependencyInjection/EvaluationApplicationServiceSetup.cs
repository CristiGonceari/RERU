using CODWER.RERU.Evaluation.Application.Models;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices;
using CODWER.RERU.Evaluation.Application.Services.GetDocumentReplacedKeysServices.Implementations;
using CODWER.RERU.Evaluation.Application.Services.GetPdfServices;
using CODWER.RERU.Evaluation.Application.Services.GetPdfServices.Implementations;
using CODWER.RERU.Evaluation.Application.Services.Implementations;
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
                .AddScoped(typeof(IGetQuestionUnitPdf), typeof(GetQuestionUnitPdf))
                .AddScoped(typeof(IGetTestPdf), typeof(GetTestPdf))
                .AddScoped(typeof(IGetTestTemplatePdf), typeof(GetTestTemplatePdf))
                .AddScoped(typeof(IPdfService), typeof(PdfService))
                .AddScoped(typeof(IOptionService), typeof(OptionService))
                .AddScoped(typeof(IGetTestTemplateDocumentReplacedKeys), typeof(GetTestTemplateDocumentReplacedKeys))
                .AddScoped(typeof(IGetTestDocumentReplacedKeys), typeof(GetTestDocumentReplacedKeys))
                .AddScoped(typeof(IInternalNotificationService), typeof(InternalNotificationService))
                .AddScoped(typeof(IAssignDocumentsAndEventsToPosition), typeof(AssignDocumentsAndEventsToPosition))
                .AddScoped(typeof(ICandidatePositionService), typeof(CandidatePositionService))
                .AddScoped(typeof(ICandidatePositionNotificationService), typeof(CandidatePositionNotificationService))
                .AddScoped(typeof(IAssignRolesToArticle), typeof(AssignRolesToArticleService))
                .AddScoped(typeof(PlatformConfig));
        }
    }
}
