using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.AddMySolicitedTest
{
    public class AddMySolicitedTestCommandHandler : IRequestHandler<AddMySolicitedTestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public AddMySolicitedTestCommandHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<int> Handle(AddMySolicitedTestCommand request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var solicitedTest = _mapper.Map<SolicitedTest>(request.Data);
            solicitedTest.UserProfileId = myUserProfile.Id;
            solicitedTest.SolicitedTestStatus = SolicitedTestStatusEnum.New;

            await _appDbContext.SolicitedTests.AddAsync(solicitedTest);
            await _appDbContext.SaveChangesAsync();

            return solicitedTest.Id;
        }
    }
}
