using System.Collections.Generic;
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using CODWER.RERU.Core.Application.Permissions;

namespace CODWER.RERU.Core.Application.Modules.GetUserModuleAccess 
{
    [ModuleOperation(permission: PermissionCodes.VIZUALIZAREA_MODULELOR_UTILIZATORULUI)]
    public class GetUserModuleAccessQuery : IRequest<List<UserModuleAccessDto>> 
    {
        public GetUserModuleAccessQuery (int id) 
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}