using System.Collections.Generic;
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.GetUserModuleAccess {
    [ModuleOperation(permission: Permissions.VIEW_USER_MODULES)]

    public class GetUserModuleAccessQuery : IRequest<List<UserModuleAccessDto>> {
        public GetUserModuleAccessQuery (int id) {
            Id = id;
        }
        public int Id { get; set; }
    }
}