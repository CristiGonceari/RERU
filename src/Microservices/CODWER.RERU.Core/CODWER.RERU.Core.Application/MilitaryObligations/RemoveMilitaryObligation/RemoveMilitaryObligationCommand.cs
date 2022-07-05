using MediatR;

namespace CODWER.RERU.Core.Application.MilitaryObligations.RemoveMilitaryObligation
{
    public class RemoveMilitaryObligationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
