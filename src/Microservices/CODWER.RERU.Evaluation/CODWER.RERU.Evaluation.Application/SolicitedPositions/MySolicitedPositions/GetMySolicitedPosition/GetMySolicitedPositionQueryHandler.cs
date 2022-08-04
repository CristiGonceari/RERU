using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.GetMySolicitedPosition
{
    public class GetMySolicitedPositionQueryHandler : IRequestHandler<GetMySolicitedPositionQuery, SolicitedCandidatePositionDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public GetMySolicitedPositionQueryHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<SolicitedCandidatePositionDto> Handle(GetMySolicitedPositionQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var solicitedTest = await _appDbContext.SolicitedVacantPositions.FirstOrDefaultAsync(x => x.Id == request.Id);
            solicitedTest.UserProfileId = myUserProfile.Id;

            return _mapper.Map<SolicitedCandidatePositionDto>(solicitedTest);
        }
    }
}
