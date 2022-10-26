using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetCheckedSolicitedVacantPositionUserFile
{
    public class GetCheckedSolicitedVacantPositionUserFileQuery : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
