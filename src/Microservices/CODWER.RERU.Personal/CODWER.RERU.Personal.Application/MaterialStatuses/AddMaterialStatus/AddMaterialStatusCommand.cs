using CODWER.RERU.Personal.DataTransferObjects.MaterialStatus;
using MediatR;

namespace CODWER.RERU.Personal.Application.MaterialStatuses.AddMaterialStatus
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
