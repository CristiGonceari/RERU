using CODWER.RERU.Core.DataTransferObjects.MilitaryObligation;
using MediatR;

namespace CODWER.RERU.Core.Application.MilitaryObligations.UpdateMilitaryObligation
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
