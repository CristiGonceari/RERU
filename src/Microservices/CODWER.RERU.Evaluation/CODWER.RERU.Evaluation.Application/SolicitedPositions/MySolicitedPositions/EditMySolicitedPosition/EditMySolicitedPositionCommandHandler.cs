using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.EditMySolicitedPosition
{
    public class EditMySolicitedPositionCommandHandler : IRequestHandler<EditMySolicitedPositionCommand, AddSolicitedCandidatePositionResponseDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public EditMySolicitedPositionCommandHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<AddSolicitedCandidatePositionResponseDto> Handle(EditMySolicitedPositionCommand request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var solicitedTest = await _appDbContext.SolicitedVacantPositions.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, solicitedTest);

            solicitedTest.UserProfileId = myUserProfile.Id;
            await _appDbContext.SaveChangesAsync();

            var solicitedVacantPosition = new AddSolicitedCandidatePositionResponseDto
            {
                SolicitedVacantPositionId = solicitedTest.Id,
                UserProfileId = solicitedTest.UserProfileId
            };

            return solicitedVacantPosition;
        }
    }
}
