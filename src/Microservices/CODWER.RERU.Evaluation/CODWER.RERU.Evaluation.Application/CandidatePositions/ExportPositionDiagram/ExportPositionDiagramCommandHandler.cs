using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.ExportPositionDiagram
{
    public class ExportPositionDiagramCommandHandler : IRequestHandler<ExportPositionDiagramCommand, FileDataDto>
    {
        private readonly IExportPositionDiagramService _exportPositionDiagramService;

        public ExportPositionDiagramCommandHandler(IExportPositionDiagramService exportPositionDiagramService)
        {
            _exportPositionDiagramService = exportPositionDiagramService;
        }

        public Task<FileDataDto> Handle(ExportPositionDiagramCommand request, CancellationToken cancellationToken)
        {
            var data = _exportPositionDiagramService.ExportPositionDiagram(request.PositionId);

            return data;
        }
    }
}
