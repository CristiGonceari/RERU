using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Data.Persistence.Context;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Identity.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetUsers {
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PaginatedModel<UserDto>> {

        private readonly IPaginationService _paginationService;
        private readonly UserManagementDbContext _userManagementDbContext;

        public GetUsersQueryHandler (IPaginationService paginationService, UserManagementDbContext userManagementDbContext) {
            _paginationService = paginationService;
            _userManagementDbContext = userManagementDbContext;
        }

        public async Task<PaginatedModel<UserDto>> Handle (GetUsersQuery request, CancellationToken cancellationToken) {
            var users = _userManagementDbContext.Users.AsQueryable ();

            users = Filter (users, request);

            var paginatedModel = _paginationService.MapAndPaginateModel<ERPIdentityUser, UserDto> (users, request);

            return paginatedModel;
        }

        private IQueryable<ERPIdentityUser> Filter (IQueryable<ERPIdentityUser> items, GetUsersQuery request) {
            // common search by name and/or lastName
            if (!string.IsNullOrEmpty (request.Keyword)) {
                var toSearch = request.Keyword.Split (' ').ToList ();

                if (toSearch.Count == 1) {
                    items = items.Where (x =>
                        x.Name.Contains (toSearch.First ()) ||
                        x.LastName.Contains (toSearch.First ()));
                }

                if (toSearch.Count == 2) {
                    items = items.Where (x =>
                        x.Name.Contains (toSearch.First ()) ||
                        x.LastName.Contains (toSearch.First ()) ||
                        x.Name.Contains (toSearch.Last ()) ||
                        x.LastName.Contains (toSearch.Last ()));
                }
            }

            return items;
        }
    }
}