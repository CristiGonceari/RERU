using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.GetTimeSheetTableValues
{
    public class GetTimeSheetTableValuesQueryHandler : IRequestHandler<GetTimeSheetTableValuesQuery, PaginatedModel<ContractorTimeSheetTableDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly ITimeSheetTableService _timeSheetTableService;
        private readonly ILoggerService<GetTimeSheetTableValuesQuery> _loggerService;
        private readonly IUserProfileService _userProfileService;

        public GetTimeSheetTableValuesQueryHandler(AppDbContext appDbContext,
            IPaginationService paginationService,
            ITimeSheetTableService timeSheetTableService,
            ILoggerService<GetTimeSheetTableValuesQuery> loggerService,
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _timeSheetTableService = timeSheetTableService;
            _loggerService = loggerService;
            _userProfileService = userProfileService;
        }
        public async Task<PaginatedModel<ContractorTimeSheetTableDto>> Handle(GetTimeSheetTableValuesQuery request, CancellationToken cancellationToken)
        {
            var contractors = _appDbContext.Contractors
                .Include(r => r.Positions)
                    .ThenInclude(p => p.Department)
                .Include(c => c.Positions)
                    .ThenInclude(p => p.OrganizationRole)
                .Include(c => c.TimeSheetTables)
                .InServiceAt(DateTime.Now)
                .Select(c => new Contractor
                {
                    Id = c.Id, 
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    FatherName = c.FatherName,
                    Positions = c.Positions,
                    TimeSheetTables = c.TimeSheetTables
                        .Where(tst =>
                            tst.Date.Date >= request.FromDate.Date
                            && tst.Date.Date <= request.ToDate.Date)
                        .ToList()
                })
                .AsQueryable();

            contractors = await FilterByDepartment(contractors, request);

            contractors = await FilterByName(request, contractors);

            var paginatedModel =
                await _paginationService.MapAndPaginateModelAsync<Contractor, ContractorTimeSheetTableDto>(contractors, request);

            var workingDays = await _timeSheetTableService.GetWorkingDays(request.FromDate, request.ToDate);

            foreach (var contractor in paginatedModel.Items)
            {
                for (var i = request.FromDate.Date; i <= request.ToDate.Date; i = i.AddDays(1))
                {
                    if (contractor.Content.All(x => x.Date.Date != i))
                    {
                        contractor.Content.Add(new TimeSheetTableDto
                        {
                            ContractorId = contractor.ContractorId,
                            Date = i
                        });
                    }
                }

                contractor.Content = contractor.Content.OrderBy(x => x.Date).ToList();

                contractor.WorkingDays = workingDays;
                contractor.WorkedHours = await _timeSheetTableService.GetWorkedHoursByTimeSheet(contractor.Content);
                contractor.FreeHours = await _timeSheetTableService.GetFreeHoursForContractor(contractor.ContractorId, contractor.WorkedHours, request.FromDate, request.ToDate, contractor.WorkingDays);
            }

            var contractorsList = paginatedModel.Items.Select(c => new ContractorTimeSheetTableDto
            {
                ContractorId = c.ContractorId,
                ContractorName = c.ContractorName,
                Department = c.Department,
                Role = c.Role,
                Content = c.Content.Select(x => new TimeSheetTableDto 
                { 
                    Date = x.Date,
                    ContractorId = c.ContractorId,
                    ValueId = x.ValueId, 
                    Value = x.Value 
                })
                .Where(x => x.Date == DateTime.Today.AddDays(-1)).ToList(),
                WorkedHours = c.WorkedHours,
                FreeHours = c.FreeHours,
                WorkingDays = c.WorkingDays

            });

            await LogAction(contractorsList);

            return paginatedModel;
        }

        private async Task LogAction(IEnumerable<ContractorTimeSheetTableDto> contractors)
        {
            await _loggerService.Log(LogData.AsPersonal($"TimeSheetTable was viewed", contractors));
        }

        private async Task<IQueryable<Contractor>> FilterByName(GetTimeSheetTableValuesQuery request, IQueryable<Contractor> contractors)
        {
            if (!string.IsNullOrEmpty(request.ContractorName))
            {
                contractors = contractors.FilterByName(request.ContractorName);
            }

            return contractors.AsQueryable();
        }

        private async Task<IQueryable<Contractor>> FilterByDepartment(IQueryable<Contractor> contractors, GetTimeSheetTableValuesQuery request)
        {
            if (request.DepartmentId != null)
            {
                contractors = contractors.Where(x =>
                    x.Positions.All(p => p.DepartmentId == request.DepartmentId));
            }

            return contractors.AsQueryable();
        }

    }
}
