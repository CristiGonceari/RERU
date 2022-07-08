using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.DeleteSolicitedVacantPositionUserFile
{
    public class DeleteSolicitedVacantPositionUserFileCommand : IRequest<Unit>
    {
        public string FileId { get; set; }
    }
}
