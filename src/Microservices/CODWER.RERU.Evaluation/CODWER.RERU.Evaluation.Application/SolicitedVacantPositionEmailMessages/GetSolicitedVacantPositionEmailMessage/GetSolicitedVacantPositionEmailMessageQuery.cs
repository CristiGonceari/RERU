using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionEmailMessages.GetSolicitedVacantPositionEmailMessage
{
    public class GetSolicitedVacantPositionEmailMessageQuery : IRequest<string>
    {
        public SolicitedVacantPositionEmailMessageEnum MessageType { get; set; }
    }
}
