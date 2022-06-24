using CODWER.RERU.Core.DataTransferObjects.MilitaryObligation;
using MediatR;

namespace CODWER.RERU.Core.Application.MilitaryObligations.AddMilitaryObligation
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
