using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfiles
{
    public class GetUserProfilesQueryHandler : IRequestHandler<GetUserProfilesQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetUserProfilesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfileDto();

            var items = _appDbContext.UserProfiles
                .Where(x => x.IsActive)
                .Include(up => up.EventResponsiblePersons)
                .Include(up => up.EventUsers)
                .Include(up => up.Role)
                .Include(up => up.Department)
                .OrderBy(up => up.LastName)
                .ThenBy(up => up.FirstName)
                .AsQueryable();

            if (currentUser.AccessModeEnum == AccessModeEnum.CurrentDepartment || currentUser.AccessModeEnum == null)
            {
                items = items.Where(x => x.DepartmentColaboratorId == currentUser.DepartmentColaboratorId);
            }
            else if (currentUser.AccessModeEnum == AccessModeEnum.OnlyCandidates)
            {
                items = items.Where(x => x.DepartmentColaboratorId == null && x.RoleColaboratorId == null);
            }
            else if (currentUser.AccessModeEnum == AccessModeEnum.AllDepartments)
            {
                items = items.Where(x => x.DepartmentColaboratorId != null);
            }

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

            if (request.DepartmentId.HasValue)
            {
                items = items.Where(x => x.Department.Id == request.DepartmentId);
            }

            if (request.RoleId.HasValue)
            {
                items = items.Where(x => x.Role.Id == request.RoleId);
            }

            if (request.ExceptUserIds.Count>0)
            {
                items = items.Where(x => !request.ExceptUserIds.Contains(x.Id));
            }

            if (request.EventUsers)
            {
                var list = _appDbContext.EventResponsiblePersons.Select(erp => erp.UserProfileId).ToList();

                foreach (var id in list)
                {
                    items = items.Where(x => x.Id != id);
                }
            }

            if (request.EventResponsiblePerson)
            {
                var list = _appDbContext.EventUsers.Select(erp => erp.UserProfileId).ToList();

                foreach (var id in list)
                {
                    items = items.Where(x => x.Id != id);
                }
            }

            if (request.UserStatusEnum.HasValue)
            {
                if (request.UserStatusEnum == UserStatusEnum.Employee)
                {
                    items = items.Where(x => x.DepartmentColaboratorId != null && x.RoleColaboratorId != null);
                }
                else if (request.UserStatusEnum == UserStatusEnum.Candidate)
                {
                    items = items.Where(x => x.DepartmentColaboratorId == null || x.RoleColaboratorId == null);
                }
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(items, request);

            return paginatedModel;
        }
    }
}
