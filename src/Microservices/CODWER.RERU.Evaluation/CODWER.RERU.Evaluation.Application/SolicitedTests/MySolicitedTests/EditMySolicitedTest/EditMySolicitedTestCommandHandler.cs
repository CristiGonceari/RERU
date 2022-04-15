using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.EditMySolicitedTest
{
    public class EditMySolicitedTestCommandHandler : IRequestHandler<EditMySolicitedTestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public EditMySolicitedTestCommandHandler(AppDbContext appDbContext, IMapper mapper, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        public async Task<int> Handle(EditMySolicitedTestCommand request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var solicitedTest = await _appDbContext.SolicitedTests.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, solicitedTest);

            solicitedTest.UserProfileId = myUserProfile.Id;
            await _appDbContext.SaveChangesAsync();

            return solicitedTest.Id;
        }
    }
}
