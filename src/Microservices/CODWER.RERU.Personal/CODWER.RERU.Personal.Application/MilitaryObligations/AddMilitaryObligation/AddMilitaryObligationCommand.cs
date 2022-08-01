using CODWER.RERU.Personal.DataTransferObjects.MilitaryObligation;
using MediatR;

namespace CODWER.RERU.Personal.Application.MilitaryObligations.AddMilitaryObligation
{
    public class AddMilitaryObligationCommand : IRequest<int>
    {
        public AddMilitaryObligationCommand(MilitaryObligationDto data)
        {
            Data = data;
        }

        public MilitaryObligationDto Data { get; set; }
    }
}
