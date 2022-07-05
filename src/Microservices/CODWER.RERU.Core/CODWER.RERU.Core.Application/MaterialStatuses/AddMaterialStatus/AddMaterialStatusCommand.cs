using CODWER.RERU.Core.DataTransferObjects.MaterialStatus;
using MediatR;

namespace CODWER.RERU.Core.Application.MaterialStatuses.AddMaterialStatus
{
    public class AddMaterialStatusCommand : IRequest<int>
    {
        public AddMaterialStatusCommand(AddEditMaterialStatusDto data)
        {
            Data = data;
        }

        public AddEditMaterialStatusDto Data { get; set; }
    }
}
