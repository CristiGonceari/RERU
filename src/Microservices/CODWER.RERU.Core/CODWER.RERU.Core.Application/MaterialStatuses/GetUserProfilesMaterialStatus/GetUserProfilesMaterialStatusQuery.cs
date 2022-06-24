using CODWER.RERU.Core.DataTransferObjects.MaterialStatus;
using MediatR;

namespace CODWER.RERU.Core.Application.MaterialStatuses.GetUserProfilesMaterialStatus
{
    public class GetUserProfilesMaterialStatusQuery : IRequest<MaterialStatusDto>
    {
        public int UserProfileId { get; set; }
    }
}
