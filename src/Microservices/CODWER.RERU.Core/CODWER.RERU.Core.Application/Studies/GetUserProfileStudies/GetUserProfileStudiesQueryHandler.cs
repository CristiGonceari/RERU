using CODWER.RERU.Core.DataTransferObjects.Studies;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Studies.GetUserProfileStudies
{
    class GetUserProfileStudiesQueryHandler : IRequestHandler<GetUserProfileStudiesQuery, PaginatedModel<StudyDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserProfileStudiesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<StudyDto>> Handle(GetUserProfileStudiesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Studies
                .Include(x => x.StudyType)
                .Where(x => x.UserProfileId == request.UserProfileId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Study, StudyDto>(items, request);

            return paginatedModel;
        }
    }
}
