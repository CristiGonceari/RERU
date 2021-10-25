
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetModuleRoleForEdit
{
    [ModuleOperation(permission: Permissions.UPDATE_MODULE_ROLE)]

    public class GetModuleRoleForEditQuery : IRequest<AddEditModuleRoleDto>
    {
        public GetModuleRoleForEditQuery(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}