using CODWER.RERU.Personal.DataTransferObjects.Contracts;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contracts.UpdateContract
{
    public class UpdateContractCommand : IRequest<Unit>
    {
        public UpdateContractCommand(UpdateIndividualContractDto data)
        {
            Data = data;
        }

        public UpdateIndividualContractDto Data { get; set; }
    }
}
