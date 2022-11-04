using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.GetLocationResponsiblePersons
{
    public class GetLocationResponsiblePersonsQueryHandler : IRequestHandler<GetLocationResponsiblePersonsQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetLocationResponsiblePersonsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetLocationResponsiblePersonsQuery request, CancellationToken cancellationToken)
        {
            var responsiblePersons = _appDbContext.LocationResponsiblePersons
                .Include(x => x.UserProfile)
                .Where(x => x.LocationId == request.LocationId)
                .Select(x => x.UserProfile)
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(responsiblePersons, request);
        }
    }
}
