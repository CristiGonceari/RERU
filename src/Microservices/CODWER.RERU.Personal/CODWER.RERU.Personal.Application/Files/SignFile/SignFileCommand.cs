using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Files.SignFile
{
    [ModuleOperation(permission: PermissionCodes.SIGN_DOCUMENTS_GENERAL_ACCESS)]
    public class SignFileCommand : IRequest<Unit>
    {
        public SignFileDto Data { get; set; }
    }
}
