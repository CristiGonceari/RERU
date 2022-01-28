using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventUsers.GetEventUsers
{
    public class GetEventUsersQueryHandler : IRequestHandler<GetEventUsersQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEventUsersQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetEventUsersQuery request, CancellationToken cancellationToken)
        {
            var eventUsers = _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.UserProfile)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(eventUsers, request);
        }
    }
}
