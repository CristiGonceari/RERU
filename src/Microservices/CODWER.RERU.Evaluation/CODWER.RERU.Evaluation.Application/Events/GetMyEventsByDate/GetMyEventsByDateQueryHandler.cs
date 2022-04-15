﻿using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.GetMyEventsByDate
{
    public class GetMyEventsByDateQueryHandler : IRequestHandler<GetMyEventsByDateQuery, PaginatedModel<EventDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public GetMyEventsByDateQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _paginationService = paginationService;
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<EventDto>> Handle(GetMyEventsByDateQuery request, CancellationToken cancellationToken)
        {
            var curUser = await _userProfileService.GetCurrentUser();

            if (curUser != null)
            {
                var myEvents = _appDbContext.Events
                    .Include(x => x.EventUsers)
                    .Include(x => x.EventTestTemplates)
                    .ThenInclude(x => x.TestTemplate)
                    .Where(x => x.EventUsers.Any(e => e.UserProfileId == curUser.Id) 
                                                    && x.EventTestTemplates.Any(e => e.TestTemplate.Mode == request.TestTemplateMode) 
                                                    && x.FromDate.Date <= request.Date && x.TillDate.Date >= request.Date)
                    .AsQueryable();

                return await _paginationService.MapAndPaginateModelAsync<Event, EventDto>(myEvents, request);
            }

            return new PaginatedModel<EventDto>(new PaginatedList<EventDto>(new List<EventDto>(), 0, 1, 10));
        }
    }
}
