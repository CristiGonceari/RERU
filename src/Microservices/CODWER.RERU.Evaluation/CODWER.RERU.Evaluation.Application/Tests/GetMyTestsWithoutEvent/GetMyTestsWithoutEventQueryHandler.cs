﻿using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.Extensions;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEvent
{
    public class GetMyTestsWithoutEventQueryHandler : IRequestHandler<GetMyTestsWithoutEventQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;

        public GetMyTestsWithoutEventQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetMyTestsWithoutEventQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                    .ThenInclude(tt => tt.Settings)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == currentUserId && t.Event == null && t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .OrderByDescending(x => x.Id)
                .DistinctBy2(x => x.HashGroupKey != null ? x.HashGroupKey : x.Id.ToString())
                .AsQueryable();


            if (request.StartTime != null && request.EndTime != null) {
                myTests = myTests.Where(p => p.StartTime >= request.StartTime && p.EndTime <= request.EndTime ||
                                                    (request.StartTime <= p.StartTime && p.StartTime <= request.EndTime) && (request.StartTime <= p.EndTime && p.EndTime >= request.EndTime) ||
                                                    (request.StartTime >= p.StartTime && p.StartTime <= request.EndTime) && (request.StartTime <= p.EndTime && p.EndTime <= request.EndTime));
            }

            return await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(myTests, request); 
        }
    }
}
