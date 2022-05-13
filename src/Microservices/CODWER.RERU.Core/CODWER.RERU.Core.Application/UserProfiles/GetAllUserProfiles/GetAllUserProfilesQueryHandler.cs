﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.UserProfiles.GetAllUserProfiles 
{
    public class GetAllUserProfilesQueryHandler : BaseHandler, IRequestHandler<GetAllUserProfilesQuery, PaginatedModel<UserProfileDto>> 
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetAllUserProfilesQueryHandler(ICommonServiceProvider commonServiceProvider, IPaginationService paginationService, 
            AppDbContext appDbContext) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<UserProfileDto>> Handle (GetAllUserProfilesQuery request, CancellationToken cancellationToken) 
        {
            var filterData = new FilterUserProfilesDto
            {
                Keyword = request.Keyword,
                Email = request.Email,
                Idnp = request.Idnp,
                Status = request.Status
            };

            var userProfiles = GetAndFilterUserProfiles.Filter(_appDbContext, filterData);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<UserProfile, UserProfileDto> (userProfiles, request);

            if (request.UserStatusEnum.HasValue)
            {
                paginatedModel.Items = paginatedModel.Items.Where(p => p.UserStatusEnum == request.UserStatusEnum);
            }

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