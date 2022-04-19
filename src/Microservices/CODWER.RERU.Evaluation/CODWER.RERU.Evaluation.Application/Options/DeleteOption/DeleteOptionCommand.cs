using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Options.DeleteOption
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_INTREBARI)]
    public class DeleteOptionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
