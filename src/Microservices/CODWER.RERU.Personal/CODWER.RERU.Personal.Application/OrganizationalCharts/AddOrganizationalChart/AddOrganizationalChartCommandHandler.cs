﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.AddOrganizationalChart
{
    public class AddOrganizationalChartCommandHandler : IRequestHandler<AddOrganizationalChartCommand, int>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddOrganizationalChartCommand> _loggerService;

        public AddOrganizationalChartCommandHandler(
                AppDbContext appDbContext, 
                IMapper mapper,
                ILoggerService<AddOrganizationalChartCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddOrganizationalChartCommand request, CancellationToken cancellationToken)
        {
            var item = _mapper.Map<OrganizationalChart>(request.Data);

            await _appDbContext.OrganizationalCharts.AddAsync(item);
            await _appDbContext.SaveChangesAsync();

            await LogAction(item);

            return item.Id;
        }

        private async Task LogAction(OrganizationalChart organizationalChart)
        {
            await _loggerService.Log(LogData.AsPersonal($@"Organigrama ""{organizationalChart.Name}"" a fost adăugată în sistem", organizationalChart));
        }
    }
}
