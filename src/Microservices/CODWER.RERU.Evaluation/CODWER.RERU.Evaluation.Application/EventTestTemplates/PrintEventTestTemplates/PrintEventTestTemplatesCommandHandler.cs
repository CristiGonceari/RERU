using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.PrintEventTestTemplates
{
    public class PrintEventTestTemplatesCommandHandler : IRequestHandler<PrintEventTestTemplatesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IExportData<TestTemplate, TestTemplateDto> _printer;

        public PrintEventTestTemplatesCommandHandler(AppDbContext appDbContext, 
            IUserProfileService userProfileService, 
            IExportData<TestTemplate, TestTemplateDto> printer)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintEventTestTemplatesCommand request, CancellationToken cancellationToken)
        {
            var eventTestTemplates = _appDbContext.EventTestTemplates
                .Include(x => x.TestTemplate)
                .Where(x => x.EventId == request.EventId)
                .OrderBy(x => x.CreateDate)
                .Select(x => x.TestTemplate)
                .AsQueryable();

            eventTestTemplates = await FilterTestTemplates(eventTestTemplates);

            var result = _printer.ExportTableSpecificFormat(new TableData<TestTemplate>
            {
                Name = request.TableName,
                Items = eventTestTemplates,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }

        public async Task<IQueryable<TestTemplate>> FilterTestTemplates(IQueryable<TestTemplate> testTemplates)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var currentModuleId = _appDbContext.GetModuleIdByPrefix(ModulePrefix.Evaluation);

            var currentUserProfile = _appDbContext.UserProfiles
                .Include(x => x.ModuleRoles)
                .ThenInclude(x => x.ModuleRole)
                .FirstOrDefault(x => x.Id == currentUserId);

            var userCurrentRole = currentUserProfile.ModuleRoles.FirstOrDefault(x => x.ModuleRole.ModuleId == currentModuleId);

            if (currentUserProfile.ModuleRoles.Contains(userCurrentRole))
            {
                testTemplates = testTemplates.Where(x => x.TestTemplateModuleRoles.Select(x => x.ModuleRole).Contains(userCurrentRole.ModuleRole) || !x.TestTemplateModuleRoles.Any());
            }

            return testTemplates;
        }
    }
}
