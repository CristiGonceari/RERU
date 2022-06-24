using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using MediatR;

namespace CODWER.RERU.Core.Application.Bulletins.UpdateBulletin
{
    public class UpdateBulletinCommand: IRequest<Unit>
    {
        public UpdateBulletinCommand(BulletinDto data)
        {
            Data = data;
        }

        public BulletinDto Data { get; set; }
    }
}
