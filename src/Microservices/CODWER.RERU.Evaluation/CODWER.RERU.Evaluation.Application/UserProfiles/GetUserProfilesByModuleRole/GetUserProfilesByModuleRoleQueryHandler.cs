using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Extensions;
using RERU.Data.Persistence.ModulePrefixes;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfilesByModuleRole
{
    public class GetUserProfilesByModuleRoleQueryHandler : IRequestHandler<GetUserProfilesByModuleRoleQuery, PaginatedModel<UserProfileDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;
        private readonly ICurrentModuleService _currentModuleService;

        public GetUserProfilesByModuleRoleQueryHandler(AppDbContext appDbContext,
            IPaginationService paginationService, 
            IUserProfileService userProfileService, 
            ICurrentModuleService currentModuleService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
            _currentModuleService = currentModuleService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetUserProfilesByModuleRoleQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfileDto();

            var items = _appDbContext.UserProfiles
                .Where(x => x.IsActive)
                .Include(up => up.EventResponsiblePersons)
                .Include(up => up.EventUsers)
                .Include(up => up.Role)
                .Include(up => up.Department)
                .Include(up => up.EmployeeFunction)
                .Include(up => up.ModuleRoles)
                .OrderByFullName()
                .AsQueryable();

            items = await FilterByAccessModeEnum(currentUser, items);

            items = await Filter(request, items);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(items, request);

            return paginatedModel;
        }

        private async Task<IQueryable<UserProfile>> FilterByAccessModeEnum(UserProfileDto currentUser, IQueryable<UserProfile> items)
        {
            switch (currentUser.AccessModeEnum)
            {
                case AccessModeEnum.All:
                    break;
                case AccessModeEnum.CurrentDepartment:
                case null:
                    items = items.Where(x => x.DepartmentColaboratorId == currentUser.DepartmentColaboratorId);
                    break;
                case AccessModeEnum.OnlyCandidates:
                    items = items.Where(x => x.DepartmentColaboratorId == null && x.RoleColaboratorId == null);
                    break;
                case AccessModeEnum.AllDepartments:
                    items = items.Where(x => x.DepartmentColaboratorId != null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return items;
        }

        private async Task<IQueryable<UserProfile>> Filter(GetUserProfilesByModuleRoleQuery request, IQueryable<UserProfile> items)
        {
            items = await FilterByModuleRole(request, items);

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

            if (request.FunctionId.HasValue)
            {
                items = items.Where(x => x.EmployeeFunction.Id == request.FunctionId);
            }

            if (request.ExceptUserIds.Count > 0)
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
                items = request.UserStatusEnum switch
                {
                    UserStatusEnum.Employee => items.Where(x =>
                        x.DepartmentColaboratorId != null && x.RoleColaboratorId != null),
                    UserStatusEnum.Candidate => items.Where(x =>
                        x.DepartmentColaboratorId == null || x.RoleColaboratorId == null),
                    _ => items
                };
            }

            return items;
        }

        private async Task<IQueryable<UserProfile>> FilterByModuleRole(GetUserProfilesByModuleRoleQuery request, IQueryable<UserProfile> items)
        {
            var testTemplate = _appDbContext.TestTemplates
                .Include(x => x.TestTemplateModuleRoles)
                .FirstOrDefault(x => x.Id == request.TestTemplateId);

            if (testTemplate != null)
            {
                var testTemplateModuleRoles = testTemplate.TestTemplateModuleRoles.Select(x => x.ModuleRoleId).ToList();

                items = items.Where(x => x.ModuleRoles.Any(md => testTemplateModuleRoles.Contains(md.ModuleRoleId)) || !testTemplateModuleRoles.Any());
            }

            return items;
        }

    }
}
