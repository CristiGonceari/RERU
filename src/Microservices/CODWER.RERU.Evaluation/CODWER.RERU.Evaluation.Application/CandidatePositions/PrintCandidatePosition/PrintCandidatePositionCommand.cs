using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.PrintCandidatePosition
{
    public class PrintCandidatePositionCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
    }
}
