using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Events.EditEvent
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class EditEventCommand : IRequest<int>
    {
        public AddEditEventDto Data { get; set; }
    }
}
