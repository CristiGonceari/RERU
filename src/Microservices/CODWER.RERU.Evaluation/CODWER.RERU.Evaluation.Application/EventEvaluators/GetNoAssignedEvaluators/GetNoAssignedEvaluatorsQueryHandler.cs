using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetNoAssignedEvaluators
{
    public class GetNoAssignedEvaluatorsQueryHandler : IRequestHandler<GetNoAssignedEvaluatorsQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;


        public GetNoAssignedEvaluatorsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetNoAssignedEvaluatorsQuery request, CancellationToken cancellationToken)
        {
            var evaluators = _appDbContext.EventEvaluators
                .Include(x => x.Evaluator)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.Evaluator.Id)
                .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles
                                            .Include(up => up.EventUsers)
                                            .AsQueryable();

             userProfiles = userProfiles.Where(x => !evaluators.Any(e => e == x.Id) && !x.EventUsers.Any(eu => eu.UserProfileId == x.Id));


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
