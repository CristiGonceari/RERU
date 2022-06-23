﻿using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.AddMySolicitedTest
{
    public class AddMySolicitedTestCommandHandler : IRequestHandler<AddMySolicitedTestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
        private readonly ILoggerService<AddMySolicitedTestCommandHandler> _loggerService;

        public AddMySolicitedTestCommandHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService, ILoggerService<AddMySolicitedTestCommandHandler> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddMySolicitedTestCommand request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var solicitedTest = _mapper.Map<SolicitedVacantPosition>(request.Data);
            solicitedTest.UserProfileId = myUserProfile.Id;
            solicitedTest.SolicitedTestStatus = SolicitedTestStatusEnum.New;

            await _appDbContext.SolicitedVacantPositions.AddAsync(solicitedTest);
            await _appDbContext.SaveChangesAsync();
            await LogAction(solicitedTest);

            return solicitedTest.Id;
        }

        private async Task LogAction(SolicitedVacantPosition item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Solicited test was created", item));
        }
    }
}
