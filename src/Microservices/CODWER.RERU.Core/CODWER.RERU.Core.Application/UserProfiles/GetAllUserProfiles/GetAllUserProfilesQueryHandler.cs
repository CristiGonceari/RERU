using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.UserProfiles.GetAllUserProfiles 
{
    public class GetAllUserProfilesQueryHandler : BaseHandler, IRequestHandler<GetAllUserProfilesQuery, PaginatedModel<UserProfileDto>> 
    {
        private readonly IPaginationService _paginationService;

        public GetAllUserProfilesQueryHandler (ICommonServiceProvider commonServiceProvider, IPaginationService paginationService) : base (commonServiceProvider) {
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle (GetAllUserProfilesQuery request, CancellationToken cancellationToken) 
        {
            var userProfiles = AppDbContext.UserProfiles.AsQueryable();

            userProfiles = Filter(userProfiles, request);
            userProfiles = Sort(userProfiles, request);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto> (userProfiles, request);

            return paginatedModel;
        }

        private IQueryable<UserProfile> Filter (IQueryable<UserProfile> items, GetAllUserProfilesQuery request) 
        {
            // common search by name and/or lastName
            if (!string.IsNullOrEmpty (request.Keyword)) {
                var toSearch = request.Keyword.Split (' ').ToList();

                foreach (var s in toSearch)
                {
                    items = items.Where(p =>
                        p.FirstName.ToLower().Contains(s.ToLower())
                        || p.LastName.ToLower().Contains(s.ToLower())
                        || p.FatherName.ToLower().Contains(s.ToLower()));
                }
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                var toSearch = request.Email.Split(' ').ToList();

                items = items.Where(p => p.Email.ToLower().Contains(toSearch.First().ToLower()));
            }

            if (!string.IsNullOrEmpty(request.Idnp))
            {
                var toSearch = request.Idnp.Split(' ').ToList();

                items = items.Where(p => p.Idnp.ToLower().Contains(toSearch.First().ToLower()));
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
                items = request.Order.Equals("asc")? items.OrderBy(up => up.LastName).ThenBy(up => up.FirstName): items.OrderByDescending(up => up.LastName).ThenByDescending(up => up.FirstName);
            }
            else if (request.Sort.Equals("username"))
            {
                items = request.Order.Equals("asc") ? items.OrderBy(up => up.Email) : items.OrderByDescending(up => up.Email);
            }         

            return items;
        }
    }
}