using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.RegistrationPageMessage.AddEditRegistrationPageMessage
{
    [ModuleOperation(permission: PermissionCodes.PAGINA_DE_INREGISTRARE)]

    public class AddEditRegistrationPageMessageCommand : IRequest<int>
    {
        public string Message { get; set; }
    }
}
