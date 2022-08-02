using CODWER.RERU.Core.DataTransferObjects.MaterialStatus;
using MediatR;

namespace CODWER.RERU.Core.Application.MaterialStatuses.GetUserProfilesMaterialStatus
{
    public class GetUserProfilesMaterialStatusQuery : IRequest<AddEditMaterialStatusDto>
    {
        public int ContractorId { get; set; }
    }
}
