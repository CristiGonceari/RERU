using CODWER.RERU.Personal.DataTransferObjects.MilitaryObligation;
using MediatR;

namespace CODWER.RERU.Personal.Application.MilitaryObligations.UpdateMilitaryObligation
{
    public class UpdateMilitaryObligationCommand : IRequest<Unit>
    {
        public UpdateMilitaryObligationCommand(MilitaryObligationDto data)
        {
            Data = data;
        }

        public MilitaryObligationDto Data { get; set; }
    }
}
