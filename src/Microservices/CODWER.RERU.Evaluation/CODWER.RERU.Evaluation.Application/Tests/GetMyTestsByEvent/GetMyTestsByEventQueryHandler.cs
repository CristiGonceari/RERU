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

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyTestsByEvent
{
    public class GetMyTestsByEventQueryHandler : IRequestHandler<GetMyTestsByEventQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetMyTestsByEventQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetMyTestsByEventQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == myUserProfile.Id && t.Event.Id == request.EventId && t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(myTests, request); 
        }
    }
}
