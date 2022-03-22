using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.SetContractorAvatar
{
    public class SetContractorAvatarCommand : IRequest<Unit>
    {
        public int ContractorId { get; set; }
        public AddFileDto FileDto { get; set; }
    }
}
