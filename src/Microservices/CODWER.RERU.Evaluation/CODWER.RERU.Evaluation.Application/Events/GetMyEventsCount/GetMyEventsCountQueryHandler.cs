﻿using CODWER.RERU.Evaluation.Application.Plans.GetCountedPlans;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            var curUser = await _userProfileService.GetCurrentUser();

             var myEvents = _appDbContext.Events
                   .Include(x => x.EventUsers)
                   .Include(x => x.EventTestTypes)
                   .ThenInclude(x => x.TestType)
                   .Where(x => x.EventUsers.Any(e => e.UserProfileId == curUser.Id) && 
                               x.EventTestTypes.Any(e => e.TestType.Mode == request.TestTypeMode))
                   .AsQueryable();

                myEvents.Where(x => x.FromDate.Date >= request.FromDate.Date && x.TillDate.Date <= request.TillDate ||
                          (request.FromDate.Date <= x.FromDate.Date && x.FromDate.Date <= request.TillDate.Date) && (request.FromDate.Date <= x.TillDate.Date && x.TillDate.Date >= request.TillDate.Date) ||
                          (request.FromDate.Date >= x.FromDate.Date && x.FromDate.Date <= request.TillDate.Date) && (request.FromDate.Date <= x.TillDate.Date && x.TillDate.Date <= request.TillDate.Date));

            var dates = new List<EventCount>();

            for (var dt = request.FromDate.Date; dt <= request.TillDate.Date; dt = dt.AddDays(1))
            {
                var count = myEvents.Where(p => p.FromDate.Date <= dt.Date && dt.Date <= p.TillDate.Date).Count();

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