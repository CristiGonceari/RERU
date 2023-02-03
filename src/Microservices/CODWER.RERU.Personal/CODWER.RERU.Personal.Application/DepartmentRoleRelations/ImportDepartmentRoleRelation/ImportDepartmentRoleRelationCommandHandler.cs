using AutoMapper;
using CODWER.RERU.Personal.Application.OrganizationalCharts.AddOrganizationalChart;
using CODWER.RERU.Personal.Application.Services;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations.ImportDepartmentRoleRelation
{
    public class ImportDepartmentRoleRelationCommandHandler : IRequestHandler<ImportDepartmentRoleRelationCommand, FileDataDto>
    {
        private readonly IImportDepartmentOrganizationalChartService _service;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<AddOrganizationalChartCommand> _loggerService;

        public ImportDepartmentRoleRelationCommandHandler(
            IImportDepartmentOrganizationalChartService service, 
            IMapper mapper, 
            AppDbContext appDbContext,
            ILoggerService<AddOrganizationalChartCommand> loggerService)
        {
            _service = service;
            _mapper = mapper;
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<FileDataDto> Handle(ImportDepartmentRoleRelationCommand request, CancellationToken cancellationToken)
        {
            var organizationalChart = _appDbContext.OrganizationalCharts.FirstOrDefault(oc => oc.Id == request.OrganizationalChartId);

            var import = await _service.ImportDepartmentToDepartmentRelation(request.Data.File, organizationalChart.Id);

            if (!import.HasError)
            {
                await _appDbContext.SaveChangesAsync();

            }
            else
            {
                _appDbContext.OrganizationalCharts.Remove(organizationalChart);

                await _appDbContext.SaveChangesAsync();

            }

            return import.File;

        }
        private async Task LogAction(OrganizationalChart organizationalChart)
        {
            await _loggerService.Log(LogData.AsPersonal($@"Organigrama ""{organizationalChart.Name}"" a fost adăugată în sistem", organizationalChart));
        }
    }
}
