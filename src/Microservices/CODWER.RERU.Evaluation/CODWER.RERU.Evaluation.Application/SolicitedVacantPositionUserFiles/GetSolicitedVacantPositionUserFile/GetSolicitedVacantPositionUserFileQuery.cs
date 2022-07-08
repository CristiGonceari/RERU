using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetSolicitedVacantPositionUserFile
{
    public class GetSolicitedVacantPositionUserFileQuery : IRequest<FileDataDto>
    {
        public string FileId { get; set; }
    }
}
