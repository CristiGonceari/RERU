using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bulletins.GetContractorBulletin
{
    public class GetContractorBulletinQuery : IRequest<BulletinsDataDto>
    {
        public int ContractorId { get; set; }
    }
}
