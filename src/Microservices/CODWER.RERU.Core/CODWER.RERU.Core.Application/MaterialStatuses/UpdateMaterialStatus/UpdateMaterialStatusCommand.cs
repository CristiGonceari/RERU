using CODWER.RERU.Core.DataTransferObjects.MaterialStatus;
using MediatR;

namespace CODWER.RERU.Core.Application.MaterialStatuses.UpdateMaterialStatus
{
    public class UpdateMaterialStatusCommand : IRequest<Unit>
    {
        public UpdateMaterialStatusCommand(AddEditMaterialStatusDto data)
        {
            Data = data;
        }

        public AddEditMaterialStatusDto Data { get; set; }
    }
}
