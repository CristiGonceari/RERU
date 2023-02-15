using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Tests.UserTests;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.GetEventTestTemplates
{
    public class GetEventTestTemplatesQueryHandler : IRequestHandler<GetEventTestTemplatesQuery, PaginatedModel<TestTemplateDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetEventTestTemplatesQueryHandler(AppDbContext appDbContext, 
            IPaginationService paginationService,
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestTemplateDto>> Handle(GetEventTestTemplatesQuery request, CancellationToken cancellationToken)
        {
            var eventTestTemplates = _appDbContext.EventTestTemplates
                .Include(x => x.TestTemplate)
                .Where(x => x.EventId == request.EventId)
                .OrderBy(x => x.EventId)
                .Select(x => x.TestTemplate)
                .AsQueryable();

            eventTestTemplates = await FilterTestTemplates(eventTestTemplates);

            return await _paginationService.MapAndPaginateModelAsync<TestTemplate, TestTemplateDto>(eventTestTemplates, request);
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
