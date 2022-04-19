using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Options.EditOption
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ÎNTREBĂRI)]
    public class EditOptionsCommand : IRequest<Unit>
    {
        public AddEditOptionDto Data { get; set; }
    }
}
