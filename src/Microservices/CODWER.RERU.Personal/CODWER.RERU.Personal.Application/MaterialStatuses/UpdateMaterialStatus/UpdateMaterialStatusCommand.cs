using CODWER.RERU.Personal.DataTransferObjects.MaterialStatus;
using MediatR;

namespace CODWER.RERU.Personal.Application.MaterialStatuses.UpdateMaterialStatus
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
