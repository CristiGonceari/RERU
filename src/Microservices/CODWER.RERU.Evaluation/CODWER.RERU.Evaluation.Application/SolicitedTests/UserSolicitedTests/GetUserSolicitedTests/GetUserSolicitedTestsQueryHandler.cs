﻿using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetUserSolicitedTests
{
    public class GetUserSolicitedTestsQueryHandler : IRequestHandler<GetUserSolicitedTestsQuery, PaginatedModel<SolicitedTestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserSolicitedTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<SolicitedTestDto>> Handle(GetUserSolicitedTestsQuery request, CancellationToken cancellationToken)
        {
            var solicitedUserTests =  _appDbContext.SolicitedVacantPositions
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Include(t => t.CandidatePosition)
                .Where(x => x.UserProfileId == request.UserId).AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<SolicitedVacantPosition, SolicitedTestDto>(solicitedUserTests, request);
        }
    }
}
