using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.EditContractorAvatar
{
    public class EditContractorAvatarCommand : IRequest<Unit>
    {
        public EditContractorAvatarDto Data { get; set; }
    }
}
