using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetEditModule 
{
    [ModuleOperation(permission: PermissionCodes.EDIT_MODULE)]
    public class GetEditModuleQuery : IRequest<ModuleDto> {
        public GetEditModuleQuery (int id) {
            Id = id;
        }

        public int Id { set; get; }
    }
}