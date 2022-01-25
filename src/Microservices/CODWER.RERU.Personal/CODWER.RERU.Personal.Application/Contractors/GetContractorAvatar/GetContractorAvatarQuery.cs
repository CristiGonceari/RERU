using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;


namespace CODWER.RERU.Personal.Application.Contractors.GetContractorAvatar
{
    public class GetContractorAvatarQuery : IRequest<ContractorAvatarDetailsDto>
    {
        public int Id { get; set; }
    }
}
