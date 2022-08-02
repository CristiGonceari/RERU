using MediatR;

namespace CODWER.RERU.Personal.Application.MilitaryObligations.RemoveMilitaryObligation
{
    public class RemoveMilitaryObligationCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
