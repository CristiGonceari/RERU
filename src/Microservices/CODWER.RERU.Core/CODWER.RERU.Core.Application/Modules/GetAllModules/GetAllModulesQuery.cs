using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetAllModules 
{
    [ModuleOperation (permission: PermissionCodes.VIEW_ALL_MODULES)]
    public class GetAllModulesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ModuleDto>> {
        public string Keyword { get; set; }
    }
}