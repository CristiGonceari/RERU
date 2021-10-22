using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetEditModule {
    [ModuleOperation(permission: Permissions.EDIT_MODULE)]
    public class GetEditModuleQuery : IRequest<ModuleDto> {
        public GetEditModuleQuery (int id) {
            Id = id;
        }
        public int Id { set; get; }
    }
}