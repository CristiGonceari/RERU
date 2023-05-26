using CODWER.RERU.Core.DataTransferObjects.Me;
using MediatR;

namespace CODWER.RERU.Core.Application.Me.GetMe
{
    public class GetMeQuery: IRequest<MeDto>
    {
        public string Authorization { get; set; }
    }
}