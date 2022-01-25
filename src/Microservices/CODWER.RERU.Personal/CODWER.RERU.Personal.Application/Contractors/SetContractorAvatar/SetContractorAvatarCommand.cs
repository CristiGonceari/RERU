using CODWER.RERU.Personal.DataTransferObjects.Avatars;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.SetContractorAvatar
{
    public class SetContractorAvatarCommand : IRequest<Unit>
    {
        public SetContractorAvatarCommand(ContractorAvatarDto dto)
        {
            Data = dto;
        }

        public ContractorAvatarDto Data { get; set; }
    }
}
