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
            var curUser = _userProfileService.GetCurrentUser();

            if (curUser != null)
            {
                var myEvents = _appDbContext.Events
                    .Include(x => x.EventUsers)
                    .Include(x => x.EventTestTypes)
                    .ThenInclude(x => x.TestType)
                    .Where(x => x.EventUsers.Any(e => e.UserProfileId == curUser.Id) && x.EventTestTypes.Any(e => e.TestType.Mode == request.TestTypeMode))
                    .AsQueryable();

                return _paginationService.MapAndPaginateModel<Event, EventDto>(myEvents, request);
            }

            return new PaginatedModel<EventDto>(new PaginatedList<EventDto>(new List<EventDto>(), 0, 1, 10));
        }
    }
}
