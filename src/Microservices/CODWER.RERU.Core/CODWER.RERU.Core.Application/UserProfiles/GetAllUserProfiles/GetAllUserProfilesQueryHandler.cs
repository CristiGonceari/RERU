using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.ServiceProvider;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.UserProfiles.GetAllUserProfiles 
{
    public class GetAllUserProfilesQueryHandler : BaseHandler, IRequestHandler<GetAllUserProfilesQuery, PaginatedModel<UserProfileDto>> 
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public GetAllUserProfilesQueryHandler(ICommonServiceProvider commonServiceProvider, IPaginationService paginationService, 
            AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle (GetAllUserProfilesQuery request, CancellationToken cancellationToken) 
        {
            var filterData = new FilterUserProfilesDto
            {
                Keyword = request.Keyword,
                Email = request.Email,
                Idnp = request.Idnp,
                Status = request.Status,
                UserStatusEnum = request.UserStatusEnum,
                DepartmentId = request.DepartmentId,
                RoleId = request.RoleId,
                FunctionId = request.FunctionId
            };

            var currentUser = await _currentUserProvider.Get();
            var userProfile = _appDbContext.UserProfiles.FirstOrDefault(x => x.Id.ToString() == currentUser.Id);
            var userProfileDto = Mapper.Map<UserProfileDto>(userProfile);

            var userProfiles = GetAndFilterUserProfiles.Filter(_appDbContext, filterData, userProfileDto);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto> (userProfiles, request);

            return paginatedModel;
        }

        private IQueryable<UserProfile> Sort(IQueryable<UserProfile> items, GetAllUserProfilesQuery request)
        {
            request.Sort = !string.IsNullOrEmpty(request.Sort) ? request.Sort : "name";
            request.Order = !string.IsNullOrEmpty(request.Order) ? request.Order : "asc";

            if (request.Sort.Equals("name"))
            {
                items = request.Order.Equals("asc") ? items.OrderBy(up => up.LastName).ThenBy(up => up.FirstName) : items.OrderByDescending(up => up.LastName).ThenByDescending(up => up.FirstName);
            }
            else if (request.Sort.Equals("username"))
            {
                items = request.Order.Equals("asc") ? items.OrderBy(up => up.Email) : items.OrderByDescending(up => up.Email);
            }

            return items;
        }
    }
}