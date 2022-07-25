using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition
{
    public class AddMySolicitedPositionCommandHandler : IRequestHandler<AddMySolicitedPositionCommand, AddSolicitedCandidatePositionResponseDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
        private readonly ILoggerService<AddMySolicitedPositionCommandHandler> _loggerService;

        public AddMySolicitedPositionCommandHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService, ILoggerService<AddMySolicitedPositionCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _loggerService = loggerService;
        }

        public async Task<AddSolicitedCandidatePositionResponseDto> Handle(AddMySolicitedPositionCommand request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var solicitedTest = _mapper.Map<SolicitedVacantPosition>(request.Data);
            solicitedTest.UserProfileId = myUserProfile.Id;
            solicitedTest.SolicitedPositionStatus = SolicitedPositionStatusEnum.New;

            await _appDbContext.SolicitedVacantPositions.AddAsync(solicitedTest);
            await _appDbContext.SaveChangesAsync();
            await LogAction(solicitedTest);

            var solicitedVacantPosition = new AddSolicitedCandidatePositionResponseDto
            {
                SolicitedVacantPositionId = solicitedTest.Id,
                UserProfileId = solicitedTest.UserProfileId
            };

            return solicitedVacantPosition;
        }

        private async Task LogAction(SolicitedVacantPosition item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Solicited test was created", item));
        }
    }
}
