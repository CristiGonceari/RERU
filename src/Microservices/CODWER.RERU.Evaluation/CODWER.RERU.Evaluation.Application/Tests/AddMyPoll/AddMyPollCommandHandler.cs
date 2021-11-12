using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddMyPoll
{
    public class AddMyPollCommandHandler : IRequestHandler<AddMyPollCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public AddMyPollCommandHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<int> Handle(AddMyPollCommand request, CancellationToken cancellationToken)
        {
            var curUser = _userProfileService.GetCurrentUser();

            var myPoll = _appDbContext.Tests.FirstOrDefault(x => x.TestTypeId == request.TestTypeId && x.UserProfileId == curUser.Id);

            if (myPoll == null)
            {
                myPoll = new Test()
                {
                    UserProfileId = curUser.Id,
                    TestTypeId = request.TestTypeId,
                    ProgrammedTime = DateTime.Now,
                    EventId = request.EventId
                };

                _appDbContext.Tests.Add(myPoll);
                await _appDbContext.SaveChangesAsync();
            }

            return myPoll.Id;
        }
    }
}
