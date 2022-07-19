using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.StaticExtensions;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CODWER.RERU.Personal.DataTransferObjects.TimeSheetTables;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.PrintAllTimeSheetTables
{
    public class PrintAllTimeSheetTableCommandHandler : IRequestHandler<PrintAllTimeSheetTableCommand, ExportTimeSheetDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ITimeSheetTableService _timeSheetTableService;

        public PrintAllTimeSheetTableCommandHandler(ITimeSheetTableService timeSheetTableService, AppDbContext appDbContext, IMapper mapper)
        { 

            _timeSheetTableService = timeSheetTableService;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ExportTimeSheetDto> Handle(PrintAllTimeSheetTableCommand request, CancellationToken cancellationToken)
        {
            var contractors = _appDbContext.Contractors
                .Include(r => r.Positions)
                .ThenInclude(p => p.Department)
                .Include(c => c.Positions)
                .ThenInclude(p => p.Role)
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

            var mappedData = _mapper.Map<List<ContractorTimeSheetTableDto>>(contractors);

            var workingDays = await _timeSheetTableService.GetWorkingDays(request.FromDate, request.ToDate);

            foreach (var contractor in mappedData)
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

            var exportFile = await _timeSheetTableService.PrintTimeSheetTableData(mappedData, request.FromDate, request.ToDate);

            return exportFile;
        }
    }
}
