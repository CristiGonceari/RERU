using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Common;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddMyPoll
{
    public class AddMyPollCommandHandler : IRequestHandler<AddMyPollCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IDateTime _dateTime;

        public AddMyPollCommandHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
            _dateTime = dateTime;
        }

        public async Task<int> Handle(AddMyPollCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myPoll = _appDbContext.Tests.FirstOrDefault(x => x.TestTemplateId == request.TestTemplateId && x.UserProfileId == currentUserId);

            if (myPoll == null)
            {
                myPoll = new Test()
                {
                    UserProfileId = currentUserId,
                    TestTemplateId = request.TestTemplateId,
                    ProgrammedTime = _dateTime.Now,
                    EventId = request.EventId
                };

                _appDbContext.Tests.Add(myPoll);
                await _appDbContext.SaveChangesAsync();
            }

            return myPoll.Id;
        }
    }
}
