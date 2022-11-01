using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyPollsCount
{
    public class GetMyPollsCountQueryHandler : IRequestHandler<GetMyPollsCountQuery, List<EventCount>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public GetMyPollsCountQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<List<EventCount>> Handle(GetMyPollsCountQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUserProfileDto();

            var myEvents = _appDbContext.EventTestTemplates
                .Include(t => t.Event)
                    .ThenInclude(t => t.EventUsers)
                .Include(t => t.TestTemplate)
                .Where(x => x.Event.EventUsers.Any(e => e.UserProfileId == currentUser.Id) &&
                            x.TestTemplate.Mode == TestTemplateModeEnum.Poll)
                .AsQueryable();

            var dates = new List<EventCount>();

            for (var dt = request.StartTime.Date; dt <= request.EndTime.Date; dt = dt.AddDays(1))
            {
                var count = myEvents.Count(p => p.Event.FromDate.Date <= dt.Date && dt.Date <= p.Event.TillDate.Date);

                var eventCount = new EventCount()
                {
                    Date = dt,
                    Count = count
                };

                dates.Add(eventCount);
            }

            return dates;
        }
    }
}
