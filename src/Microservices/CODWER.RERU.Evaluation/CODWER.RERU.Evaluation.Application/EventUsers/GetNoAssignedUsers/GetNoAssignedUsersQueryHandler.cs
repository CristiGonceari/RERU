using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventUsers.GetNoAssignedUsers
{
    public class GetNoAssignedUsersQueryHandler : IRequestHandler<GetNoAssignedUsersQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;


        public GetNoAssignedUsersQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;

        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetNoAssignedUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.UserProfile.Id)
                .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles.Include(up => up.EventResponsiblePersons).AsQueryable();

            var eventEvaluators = _appDbContext.EventEvaluators.AsQueryable();

            userProfiles = userProfiles.Where(x => !users.Any(s => s == x.Id)! && !x.EventResponsiblePersons.Any(eu => eu.UserProfileId == x.Id) && !eventEvaluators.Any(u => u.EvaluatorId == x.Id) );

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                userProfiles = userProfiles.Where(x => x.FirstName.Contains(request.FirstName));
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                userProfiles = userProfiles.Where(x => x.LastName.Contains(request.LastName));
            }

            if (!string.IsNullOrEmpty(request.FatherName))
            {
                userProfiles = userProfiles.Where(x => x.FatherName.Contains(request.FatherName));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                userProfiles = userProfiles.Where(x => x.Email.Contains(request.Email));
            }

            if (!string.IsNullOrEmpty(request.Idnp))
            {
                userProfiles = userProfiles.Where(x => x.Idnp.Contains(request.Idnp));
            }

            return await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(userProfiles, request);

        }
    }
}
