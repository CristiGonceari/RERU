using System;
using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.Extensions;

namespace CODWER.RERU.Evaluation.Application.EventUsers.GetEventAssignedUsers
{
    public class GetEventAssignedUsersQueryHandler : IRequestHandler<GetEventAssignedUsersQuery, PaginatedModel<UserProfileDto>>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEventAssignedUsersQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle(GetEventAssignedUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                    .ThenInclude(x => x.Department)
                .Include(x => x.UserProfile)
                     .ThenInclude(x => x.Role)
                .Where(x => x.EventId == request.EventId)
                .AsQueryable();

            var userProfiles = _appDbContext.UserProfiles
                .Include(up => up.Role)
                .Include(up => up.Department)
                .OrderByFullName()
                .AsQueryable();

            userProfiles = userProfiles.Where(up => users.Any(eu => eu.UserProfileId == up.Id));

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
