using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetAllModules {
    [ModuleOperation (permission: Permissions.VIEW_ALL_MODULES)]
    public class GetAllModulesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ModuleDto>> {
        public string Keyword { get; set; }
    }
}