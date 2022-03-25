using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Enums;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
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

        public GetContractorsQueryHandler(
            AppDbContext appDbContext, 
            IPaginationService paginationService,
            ILoggerService<GetContractorsQuery> loggerService,
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _loggerService = loggerService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<ContractorDto>> Handle(GetContractorsQuery request, CancellationToken cancellationToken)
        {

            var contractors = _appDbContext.Contractors
                .Include(r => r.Positions)
                .ThenInclude(p => p.Department)
                .Include(c => c.Positions)
                .ThenInclude(p => p.OrganizationRole)
                .Include(r => r.Contacts)
                .AsQueryable();

            contractors = Filter(contractors, request);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Contractor, ContractorDto>(contractors, request);
            
            await LogAction(contractors);

            return paginatedModel;
        }

        private async Task LogAction(IQueryable<Contractor> contractors)
        {
            await _loggerService.Log(LogData.AsPersonal($"Contractors list was viewed", contractors));
        }

        private IQueryable<Contractor> Filter(IQueryable<Contractor> items, GetContractorsQuery request)
        {
            
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                items = items.FilterByName(request.Keyword);
            }

            // search by current employers state

            var now = DateTime.Now;

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

            if (request.OrganizationRoleId != null)
            {
                items = items.Where(x => x.Positions.Any())
                    .Where(x => x.Positions.OrderByDescending(p => p.FromDate).First().OrganizationRoleId == request.OrganizationRoleId);
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
