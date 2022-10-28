using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.GetMyEventsCount
{
    public class GetMyEventsCountQueryHandler : IRequestHandler<GetMyEventsCountQuery, List<EventCount>>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        public GetMyEventsCountQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<List<EventCount>> Handle(GetMyEventsCountQuery request, CancellationToken cancellationToken)
        {
            var curUser = await _userProfileService.GetCurrentUserProfileDto();

             var myEvents = _appDbContext.Events
                   .Include(x => x.EventUsers)
                   .Include(x => x.EventTestTemplates)
                   .ThenInclude(x => x.TestTemplate)
                   .Where(x => x.EventUsers.Any(e => e.UserProfileId == curUser.Id) && 
                               x.EventTestTemplates.Any(e => e.TestTemplate.Mode == request.TestTemplateMode))
                   .AsQueryable();

            myEvents.Where(p => (request.FromDate.Date <= p.FromDate.Date && p.TillDate.Date <= p.TillDate.Date) ||
                                                (request.FromDate.Date <= p.FromDate.Date && p.FromDate.Date <= request.TillDate.Date && p.TillDate.Date >= request.TillDate.Date) ||
                                                (p.FromDate.Date <= request.FromDate.Date && request.FromDate.Date <= p.TillDate.Date && p.TillDate.Date <= request.TillDate.Date) ||
                                                (request.FromDate.Date >= p.FromDate.Date && p.TillDate.Date >= request.TillDate.Date));
                                       

            var dates = new List<EventCount>();

            for (var dt = request.FromDate.Date; dt <= request.TillDate.Date; dt = dt.AddDays(1))
            {
                var count = myEvents.Count(p => p.FromDate.Date <= dt.Date && dt.Date <= p.TillDate.Date);

                var planCount = new EventCount()
                {
                    Date = dt,
                    Count = count
                };

                dates.Add(planCount);
            }

            return dates;

        }
    }
}
