using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Provider;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.GetAllUserProfiles {
    public class GetAllUserProfilesQueryHandler : BaseHandler, IRequestHandler<GetAllUserProfilesQuery, PaginatedModel<UserProfileDto>> {
        private readonly IPaginationService _paginationService;

        public GetAllUserProfilesQueryHandler (ICommonServiceProvider commonServiceProvider, IPaginationService paginationService) : base (commonServiceProvider) {
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle (GetAllUserProfilesQuery request, CancellationToken cancellationToken) {
            var userProfiles = CoreDbContext.UserProfiles.AsQueryable ();

            userProfiles = Filter (userProfiles, request);
            userProfiles = Sort(userProfiles, request);

            var paginatedModel = _paginationService.MapAndPaginateModel<UserProfile, UserProfileDto> (userProfiles, request);

            return paginatedModel;
        }

        private IQueryable<UserProfile> Filter (IQueryable<UserProfile> items, GetAllUserProfilesQuery request) {
            // common search by name and/or lastName
            if (!string.IsNullOrEmpty (request.Keyword)) {
                var toSearch = request.Keyword.Split (' ').ToList ();

                foreach (var s in toSearch)
                {
                    items = items.Where(p => p.Name.Contains(s) || p.LastName.Contains(s) || p.Email.Contains(s));
                }
            }

            if (request.Status.HasValue)
            {
                items = items.Where(p => p.IsActive == request.Status.Value);
            }

            return items;
        }

        private IQueryable<UserProfile> Sort(IQueryable<UserProfile> items, GetAllUserProfilesQuery request)
        {
            request.Sort = !string.IsNullOrEmpty(request.Sort) ? request.Sort : "name";
            request.Order = !string.IsNullOrEmpty(request.Order) ? request.Order : "asc";

            if(request.Sort.Equals("name"))
            {
                items = request.Order.Equals("asc")? items.OrderBy(up => up.LastName).ThenBy(up => up.Name): items.OrderByDescending(up => up.LastName).ThenByDescending(up => up.Name);
            }
            else if (request.Sort.Equals("username"))
            {
                items = request.Order.Equals("asc") ? items.OrderBy(up => up.Email) : items.OrderByDescending(up => up.Email);
            }         

            return items;
        }
    }
}