using CODWER.RERU.Personal.DataTransferObjects.Contracts;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contracts.GetContract
{
    public class GetContractQuery : IRequest<IndividualContractDetails>
    {
        public int ContractorId { get; set; }
    }
}
