using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfiles
{
    public class GetUserProfilesQueryHandler : IRequestHandler<GetUserProfilesQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserProfilesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.UserProfiles.AsQueryable();

            if (!string.IsNullOrEmpty(request.FirstName))
            {
                items = items.Where(x => x.FirstName.ToLower().Contains(request.FirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                items = items.Where(x => x.LastName.ToLower().Contains(request.LastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.FatherName))
            {
                items = items.Where(x => x.FatherName.ToLower().Contains(request.FatherName.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                items = items.Where(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Idnp))
            {
                items = items.Where(x => x.Idnp.Contains(request.Idnp));
            }

            if (request.ExceptUserIds.Count>0)
            {
                items = items.Where(x => !request.ExceptUserIds.Contains(x.Id));
            }

            return await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(items, request);
        }
    }
}
