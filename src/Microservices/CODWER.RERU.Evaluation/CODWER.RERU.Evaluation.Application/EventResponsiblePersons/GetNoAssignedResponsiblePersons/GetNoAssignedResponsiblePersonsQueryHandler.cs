using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetNoAssignedResponsiblePersons
{
    public class GetNoAssignedResponsiblePersonsQueryHandler : IRequestHandler<GetNoAssignedResponsiblePersonsQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetNoAssignedResponsiblePersonsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetNoAssignedResponsiblePersonsQuery request, CancellationToken cancellationToken)
        {
            var responsiblePersons = _appDbContext.EventResponsiblePersons
                .Include(x => x.UserProfile)
                    .Where(x => x.EventId == request.EventId)
                    .Select(x => x.UserProfile.Id)
                    .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles
                                            .Include(up => up.EventUsers)
                                            .AsQueryable();

            userProfiles = userProfiles.Where(x => !responsiblePersons.Any(s => s == x.Id) && !x.EventUsers.Any(eu => eu.UserProfileId == x.Id));

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                userProfiles = userProfiles.Where(x => x.FirstName.Contains(request.FirstName));
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                userProfiles = userProfiles.Where(x => x.LastName.Contains(request.LastName));
            }

            if (!string.IsNullOrEmpty(request.Patronymic))
            {
                userProfiles = userProfiles.Where(x => x.Patronymic.Contains(request.Patronymic));
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
