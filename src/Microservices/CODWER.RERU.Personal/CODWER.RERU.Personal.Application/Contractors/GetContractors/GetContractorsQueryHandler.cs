﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Enums;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.StaticExtensions;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Common;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractors
{
    public class GetContractorsQueryHandler : IRequestHandler<GetContractorsQuery, PaginatedModel<ContractorDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly ILoggerService<GetContractorsQuery> _loggerService;
        private readonly IUserProfileService _userProfileService;
        private readonly IDateTime _dateTime;

        public GetContractorsQueryHandler(
            AppDbContext appDbContext, 
            IPaginationService paginationService,
            ILoggerService<GetContractorsQuery> loggerService,
            IUserProfileService userProfileService, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _loggerService = loggerService;
            _userProfileService = userProfileService;
            _dateTime = dateTime;
        }

        public async Task<PaginatedModel<ContractorDto>> Handle(GetContractorsQuery request, CancellationToken cancellationToken)
        {

            var contractors = _appDbContext.Contractors
                .Include(c => c.UserProfile)
                .Include(r => r.Positions)
                .ThenInclude(p => p.Department)
                .Include(c => c.Positions)
                .ThenInclude(p => p.Role)
                .Include(r => r.Contacts)
                .AsQueryable();

            contractors = Filter(contractors, request);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Contractor, ContractorDto>(contractors, request);
            
            await LogAction(paginatedModel.Items);

            return paginatedModel;
        }

        private async Task LogAction(IEnumerable<ContractorDto> contractors)
        {
            await _loggerService.Log(LogData.AsPersonal($"Lista de angajați a fost vizualizata", contractors));
        }

        private IQueryable<Contractor> Filter(IQueryable<Contractor> items, GetContractorsQuery request)
        {
            
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                items = items.FilterByName(request.Keyword);
            }

            // search by current employers state

            var now = _dateTime.Now;

            if (request.EmployerStates == EmployersStateEnum.InService)
            {
                items = items.InServiceAt(now);
            }
            else if (request.EmployerStates == EmployersStateEnum.Dismissed)
            {
                items = items.DismissedAt(now);
            }

            if (request.EmploymentDateFrom != null)
            {
                items = items.Where(x => x.Positions.Any())
                    .Where(x => x.Positions.OrderByDescending(p => p.FromDate).First().FromDate >= request.EmploymentDateFrom);
            }

            if (request.EmploymentDateTo != null)
            {
                items = items.Where(x => x.Positions.Any())
                    .Where(x => x.Positions.OrderByDescending(p => p.FromDate).First().FromDate <= request.EmploymentDateTo);
            }

            if (request.RoleId != null)
            {
                items = items.Where(x => x.Positions.Any())
                    .Where(x => x.Positions.OrderByDescending(p => p.FromDate).First().RoleId == request.RoleId);
            }

            if (request.DepartmentId != null)
            {
                items = items.Where(x =>
                    x.Positions.All(p => p.DepartmentId == request.DepartmentId));
            }

            return items;
        }
    }
}
