using CODWER.RERU.Personal.DataTransferObjects.Contracts;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contracts.AddContract
{
    public class AddContractCommand : IRequest<int>
    {
        public AddContractCommand(AddIndividualContractDto dto)
        {
            Data = dto;
        }

        public AddIndividualContractDto Data { get; set; }
    }
}