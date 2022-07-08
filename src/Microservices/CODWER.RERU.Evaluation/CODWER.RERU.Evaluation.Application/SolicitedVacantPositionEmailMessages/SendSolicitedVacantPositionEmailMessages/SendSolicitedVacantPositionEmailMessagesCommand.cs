using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionEmailMessages.SendSolicitedVacantPositionEmailMessages
{
    public class SendSolicitedVacantPositionEmailMessagesCommand : IRequest<int>
    {
        public string EmailMessage { get; set; }
        public int SolicitedVacantPositionId { get; set; }
        public SolicitedVacantPositionEmailMessageEnum Result { get; set; }
    }
}
