using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplates
{
    public class GetTestTemplatesQueryHandler : IRequestHandler<GetTestTemplatesQuery, PaginatedModel<TestTemplateDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetTestTemplatesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestTemplateDto>> Handle(GetTestTemplatesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUser();

            var testTemplates = GetAndFilterTestTemplates.Filter(_appDbContext, request.Name, request.EventName, request.Status, request.Mode, currentUser);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<TestTemplate, TestTemplateDto>(testTemplates, request);

            return paginatedModel;
        }
    }
}
