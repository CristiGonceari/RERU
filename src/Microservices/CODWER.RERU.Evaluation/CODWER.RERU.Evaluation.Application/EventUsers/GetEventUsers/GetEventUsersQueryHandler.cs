using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

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

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(eventUsers, request);

            return await CheckIfHasCandidatePosition(paginatedModel, request.EventId);
        }

        private async Task<PaginatedModel<UserProfileDto>> CheckIfHasCandidatePosition(PaginatedModel<UserProfileDto> paginatedModel, int eventId)
        {
            foreach (var item in paginatedModel.Items)
            {
                var eventUser = _appDbContext.EventUsers.FirstOrDefault(x => x.EventId == eventId && x.UserProfileId == item.Id);

                var eventUserCandidatePositions =
                    _appDbContext.EventUserCandidatePositions.Where(x => x.EventUserId == eventUser.Id)
                        .Select(x => x.CandidatePositionId)
                        .ToList();

                if (!(eventUser?.PositionId > 0)) continue;
                {
                    var candidatePositionNames = 
                            _appDbContext.CandidatePositions.Where(p => !eventUserCandidatePositions.All(p2 => p2 != p.Id))
                        .Select(x => x.Name)
                        .ToList();

                    item.CandidatePositionNames = candidatePositionNames;
                }
            }

            return paginatedModel;
        }
    }
}
