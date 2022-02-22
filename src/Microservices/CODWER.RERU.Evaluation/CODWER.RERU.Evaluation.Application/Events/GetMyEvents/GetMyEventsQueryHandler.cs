using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Events.GetMyEvents
{
    public class GetMyEventsQueryHandler : IRequestHandler<GetMyEventsQuery, PaginatedModel<EventDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public GetMyEventsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<EventDto>> Handle(GetMyEventsQuery request, CancellationToken cancellationToken)
        {
            var curUser = await _userProfileService.GetCurrentUser();

            if (curUser != null)
            {
                var myEvents = _appDbContext.Events
                    .Include(x => x.EventUsers)
                    .Include(x => x.EventTestTemplates)
                    .ThenInclude(x => x.TestTemplate)
                    .Where(x => x.EventUsers.Any(e => e.UserProfileId == curUser.Id) && x.EventTestTemplates.Any(e => e.TestTemplate.Mode == request.TestTemplateMode))
                    .AsQueryable();

                if (request.FromDate != null && request.TillDate != null) 
                {
                    myEvents = myEvents.Where(p => p.FromDate >= request.FromDate && p.TillDate <= request.TillDate ||
                                                    (request.FromDate <= p.FromDate && p.FromDate <= request.TillDate) && (request.FromDate <= p.TillDate && p.TillDate >= request.TillDate) ||
                                                    (request.FromDate >= p.FromDate && p.FromDate <= request.TillDate) && (request.FromDate <= p.TillDate && p.TillDate <= request.TillDate));
                }

                return await _paginationService.MapAndPaginateModelAsync<Event, EventDto>(myEvents, request);
            }

            return new PaginatedModel<EventDto>(new PaginatedList<EventDto>(new List<EventDto>(), 0, 1, 10));
        }
    }
}
