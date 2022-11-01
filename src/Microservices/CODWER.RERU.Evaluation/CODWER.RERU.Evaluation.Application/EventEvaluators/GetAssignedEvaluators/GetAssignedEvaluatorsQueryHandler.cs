﻿using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetAssignedEvaluators
{
    public class GetAssignedEvaluatorsQueryHandler : IRequestHandler<GetAssignedEvaluatorsQuery, PaginatedModel<UserProfileDto>>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetAssignedEvaluatorsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
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
                                            .AsQueryable();

            userProfiles = userProfiles.Where(up => evaluators.Any(e => e.EvaluatorId == up.Id));

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

            if (request.UserStatusEnum.HasValue)
            {
                userProfiles = request.UserStatusEnum switch
                {
                    UserStatusEnum.Employee => userProfiles.Where(x =>
                        x.DepartmentColaboratorId != null && x.RoleColaboratorId != null),
                    UserStatusEnum.Candidate => userProfiles.Where(x =>
                        x.DepartmentColaboratorId == null || x.RoleColaboratorId == null),
                    _ => userProfiles
                };
            }

            return await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto>(userProfiles, request);
        }
    }
}
