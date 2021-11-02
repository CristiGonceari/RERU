using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Services.Implementations;
using CODWER.RERU.Personal.Application.Services.VacationInterval;
using CVU.ERP.Module.Common.Providers;
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
                .AddScoped(typeof(IModulePermissionProvider), typeof(ModulePermissionProvider))
                //.AddScoped(typeof(IUserProfileService), typeof(UserProfileService))
                .AddScoped(typeof(IContractorService), typeof(ContractorService))
                .AddScoped(typeof(IVacationIntervalService), typeof(VacationIntervalService))

                //.AddScoped(typeof(IRecordMapper), typeof(RecordMapper))
                //.AddScoped(typeof(ITemplateConvertor), typeof(TemplateConvertor))
                //.AddScoped(typeof(IExcelImporter), typeof(ExcelImporter))
                //.AddScoped(typeof(IVacationTemplateParserService), typeof(VacationTemplateParserService))

                //.AddScoped(typeof(IDismissalTemplateParserService), typeof(DismissalTemplateParserService))
                //.AddScoped(typeof(ITimeSheetTableService), typeof(TimeSheetTableService))
                //.AddScoped(typeof(IStorageFileService), currentEnvironment.IsDevelopment() ? typeof(CloudStorageFileService) : typeof(DbStorageFileService))
                ;
        }
    }
}
