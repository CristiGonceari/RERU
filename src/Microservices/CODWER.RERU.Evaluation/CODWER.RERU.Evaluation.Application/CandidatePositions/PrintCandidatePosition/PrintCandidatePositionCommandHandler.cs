using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.PrintCandidatePosition
{
    public class PrintCandidatePositionCommandHandler : IRequestHandler<PrintCandidatePositionCommand, FileDataDto>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IExportData<CandidatePosition, CandidatePositionDto> _printer;
        private readonly ICandidatePositionService _candidatePositionService;
        private readonly IMapper _mapper;

        public PrintCandidatePositionCommandHandler(
            AppDbContext appDbContext, 
            IExportData<CandidatePosition, CandidatePositionDto> printer, 
            ICandidatePositionService candidatePositionService,
            IMapper mapper)
        {
            _appDbContext = appDbContext;
            _printer = printer;
            _candidatePositionService = candidatePositionService;
            _mapper = mapper;
        }

        public async Task<FileDataDto> Handle(PrintCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var filterData = new PositionFiltersDto()
            {
                Name = request.Name,
                ResponsiblePersonName = request.ResponsiblePersonName,
                MedicalColumn = request.MedicalColumn,
                ActiveFrom = request.ActiveFrom,
                ActiveTo = request.ActiveTo
            };

            var positions = GetAndPrintCandidatePosition.Filter(_appDbContext, filterData);
            var positionsDto = _mapper.Map<List<CandidatePositionDto>>(positions);

            positionsDto = await GetResponsiblePersonName(positionsDto, positions);

            var result = _printer.ExportTableSpecificFormatList(new TableListData<CandidatePositionDto>
            {
                Name = request.TableName,
                Items = positionsDto,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }

        private async Task<List<CandidatePositionDto>> GetResponsiblePersonName(List<CandidatePositionDto> positionsDto, IEnumerable<CandidatePosition> candidatePositions)
        {
            var positions = candidatePositions.ToList();

            foreach (var item in positionsDto)
            {
                var position = positions.FirstOrDefault(x => x.Id == item.Id);

                item.ResponsiblePerson = _candidatePositionService.GetResponsiblePersonName(int.Parse(position?.CreateById ?? "0"));
                item.ResponsiblePersonId = int.Parse(position?.CreateById ?? "0");
                item.Events = await GetAttachedEvents(item.Id);
            }

            return positionsDto;
        }

        private async Task<List<SelectItem>> GetAttachedEvents(int candidatePositionId) => await _appDbContext.EventVacantPositions
                .Include(x => x.Event)
                .Where(x => x.CandidatePositionId == candidatePositionId)
                .Select(e => _mapper.Map<SelectItem>(new Event
                {
                    Id = e.Event.Id,
                    Name = e.Event.Name
                }))
                .ToListAsync();
    }
}
