using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.GetMySolicitedTests
{
    public class GetMySolicitedPositionQueryHandler : IRequestHandler<GetMySolicitedPositionQuery, PaginatedModel<SolicitedCandidatePositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;
        private readonly IMapper _mapper;

        public GetMySolicitedPositionQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
            _mapper = mapper;
        }

        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> Handle(GetMySolicitedPositionQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var mySolicitedTests = _appDbContext.SolicitedVacantPositions
                .Include(t => t.UserProfile)
                .Include(t => t.CandidatePosition)
                .Include(x => x.SolicitedVacantPositionUserFiles)
                .Include(x => x.CandidatePosition.RequiredDocumentPositions)
                .Where(t => t.UserProfileId == myUserProfile.Id)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<SolicitedVacantPosition, SolicitedCandidatePositionDto>(mySolicitedTests, request);

            foreach (var item in paginatedModel.Items)
            {
                item.Events = await GetAttachedEvents(item.CandidatePositionId);
            }

            return paginatedModel;
        }

        private async Task<List<EventsWithTestTemplateDto>> GetAttachedEvents(int candidatePositionId) => await _appDbContext.EventVacantPositions
               .Include(x => x.Event)
               .Where(x => x.CandidatePositionId == candidatePositionId)
               .Select(e => _mapper.Map<EventsWithTestTemplateDto>(e))
               .ToListAsync();
    }
}
