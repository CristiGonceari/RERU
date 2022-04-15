﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.GetUserEvents
{
    public class GetUserEventsQueryHandler : IRequestHandler<GetUserEventsQuery, PaginatedModel<EventDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;

        public GetUserEventsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<EventDto>> Handle(GetUserEventsQuery request, CancellationToken cancellationToken)
        {
            var userEvents = _appDbContext.Events
                .Include(x => x.EventUsers)
                .Include(x => x.EventTestTemplates)
                .ThenInclude(x => x.TestTemplate)
                .Where(x => x.EventUsers.Any(e => e.UserProfileId == request.UserId) && x.EventTestTemplates.Any(e => e.TestTemplate.Mode == request.TestTemplateMode))
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Event, EventDto>(userEvents, request);

            return paginatedModel;
        }
    }
}
