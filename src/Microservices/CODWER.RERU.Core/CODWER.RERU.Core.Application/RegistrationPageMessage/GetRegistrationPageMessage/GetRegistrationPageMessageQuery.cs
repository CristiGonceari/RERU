using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.RegistrationPageMessage.GetRegistrationPageMessage
{
    [ModuleOperation(permission: PermissionCodes.PAGINA_DE_INREGISTRARE)]

    public class GetRegistrationPageMessageQuery : IRequest<string>
    {
    }
}
