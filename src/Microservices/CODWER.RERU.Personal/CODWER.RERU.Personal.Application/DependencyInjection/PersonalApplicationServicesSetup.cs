using CODWER.RERU.Personal.Application.NomenclatureTypes.Services;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Services.Implementations;
using CODWER.RERU.Personal.Application.Services.VacationInterval;
using CODWER.RERU.Personal.Application.TemplateParsers;
using CODWER.RERU.Personal.Application.TemplateParsers.Implementations;
using CVU.ERP.Module.Common.Providers;
using CVU.ERP.StorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CODWER.RERU.Personal.Application.DependencyInjection
{
    public static class PersonalApplicationServicesSetup
    {
        public static IServiceCollection AddPersonalApplication(this IServiceCollection services, IHostingEnvironment currentEnvironment)
        {
            services.ConfigureValidatorServices(currentEnvironment);

            return services;
        }

        public static void ConfigureValidatorServices(this IServiceCollection services, IHostingEnvironment currentEnvironment)
        {
            services
                .AddScoped(typeof(IModulePermissionProvider), typeof(Permissions.ModulePermissionProvider))
                .AddScoped(typeof(IUserProfileService), typeof(UserProfileService))
                .AddScoped(typeof(IContractorService), typeof(ContractorService))
                .AddScoped(typeof(IVacationIntervalService), typeof(VacationIntervalService))

                .AddScoped(typeof(IRecordMapper), typeof(RecordMapper))
                .AddScoped(typeof(ITemplateConvertor), typeof(TemplateConvertor))
                .AddScoped(typeof(IExcelImporter), typeof(ExcelImporter))
                .AddScoped(typeof(IVacationTemplateParserService), typeof(VacationTemplateParserService))

                .AddScoped(typeof(IDismissalTemplateParserService), typeof(DismissalTemplateParserService))
                .AddScoped(typeof(ITimeSheetTableService), typeof(TimeSheetTableService))
                .AddScoped(typeof(IDocumentTemplateReplaceKeysService), typeof(DocumentTemplateReplaceKeysService))
                .AddScoped(typeof(IImportDepartmentsAndRolesService), typeof(ImportDepartmentsAndRolesService))
                .AddScoped(typeof(IPersonalStorageClient), typeof(PersonalStorageClient));
                ;
        }
    }
}
