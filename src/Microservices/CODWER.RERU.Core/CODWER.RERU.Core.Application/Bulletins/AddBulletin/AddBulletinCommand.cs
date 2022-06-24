using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using MediatR;

namespace CODWER.RERU.Core.Application.Bulletins.AddBulletin
{
    public class AddBulletinCommand : IRequest<int>
    {
        public AddBulletinCommand(BulletinDto data)
        {
            Data = data;
        }
        public BulletinDto Data { get; set; }
    }
}
