using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfilesByModuleRole;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetAssignedEvaluators
{
    public class GetAssignedEvaluatorsQueryHandler : IRequestHandler<GetAssignedEvaluatorsQuery, PaginatedModel<UserProfileDto>>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetAssignedEvaluatorsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetAssignedEvaluatorsQuery request, CancellationToken cancellationToken)
        {
            var evaluators = _appDbContext.EventEvaluators
                .Include(x => x.Evaluator)
                .Where(x => x.EventId == request.EventId)
                .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles
                                            .Include(up => up.Role)
                                            .Include(up => up.Department)
                                            .Include(up => up.EventUsers)
                                            .OrderBy(x => x.LastName)
                                            .ThenBy(x => x.FirstName)
                                            .AsQueryable();

            userProfiles = userProfiles.Where(up => evaluators.Any(e => e.EvaluatorId == up.Id));

            userProfiles = await FilterByModuleRole(request, userProfiles);
            userProfiles = await Filter(request, userProfiles);
            userProfiles = await FilterByUserStatusEnum(request, userProfiles);

            return await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(userProfiles, request);
        }

        private async Task<IQueryable<UserProfile>> Filter(GetAssignedEvaluatorsQuery request, IQueryable<UserProfile> userProfiles)
        {
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

            if (request.DepartmentId.HasValue)
            {
                userProfiles = userProfiles.Where(x => x.Department.Id == request.DepartmentId);
            }

            if (request.RoleId.HasValue)
            {
                userProfiles = userProfiles.Where(x => x.Role.Id == request.RoleId);
            }

            return userProfiles;
        }

        private async Task<IQueryable<UserProfile>> FilterByUserStatusEnum(GetAssignedEvaluatorsQuery request, IQueryable<UserProfile> items)
        {
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

        private async Task<IQueryable<UserProfile>> FilterByModuleRole(GetAssignedEvaluatorsQuery request, IQueryable<UserProfile> items)
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
