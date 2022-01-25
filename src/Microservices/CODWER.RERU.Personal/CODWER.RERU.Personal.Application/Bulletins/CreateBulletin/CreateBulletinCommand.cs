using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bulletins.CreateBulletin
{
    public class CreateBulletinCommand : IRequest<int>
    {
        public CreateBulletinCommand(BulletinsDataDto data)
        {
            Data = data;
        }

        public BulletinsDataDto Data { get; set; }
    }
}
