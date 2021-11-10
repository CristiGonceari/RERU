using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetEventResponsiblePersons
{
    public class GetEventResponsiblePersonsQueryHandler : IRequestHandler<GetEventResponsiblePersonsQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEventResponsiblePersonsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetEventResponsiblePersonsQuery request, CancellationToken cancellationToken)
        {
            var responsiblePersons = _appDbContext.EventResponsiblePersons
                .Include(x => x.UserProfile)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.UserProfile)
                .AsQueryable();

            return _paginationService.MapAndPaginateModel<UserProfile, UserProfileDto>(responsiblePersons, request);
        }
    }
}
