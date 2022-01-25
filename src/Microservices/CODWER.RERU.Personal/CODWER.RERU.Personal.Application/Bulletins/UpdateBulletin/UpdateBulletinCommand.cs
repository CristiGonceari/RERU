using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bulletins.UpdateBulletin
{
    public class UpdateBulletinCommand : IRequest<Unit>
    {
        public UpdateBulletinCommand(BulletinsDataDto data)
        {
            Data = data;
        }

        public BulletinsDataDto Data { get; set; }
    }
}
