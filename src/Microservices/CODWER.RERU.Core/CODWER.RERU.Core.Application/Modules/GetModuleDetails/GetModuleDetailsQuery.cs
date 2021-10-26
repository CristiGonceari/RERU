using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetModuleDetails 
{
    [ModuleOperation(permission: PermissionCodes.VIEW_MODULE_DETAILS)]

    public class GetModuleDetailsQuery : IRequest<ModuleDto> 
    {
        public GetModuleDetailsQuery(int id)
        {
            Id = id;
        }

        public int Id { set; get; }
    }
}