using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using MediatR;

namespace CODWER.RERU.Core.Application.Bulletins.GetUserProfileBulletin
{
    public class GetUserProfileBulletinQuery : IRequest<BulletinDto>
    {

        public int ContractorId { get; set; }
    }
}
