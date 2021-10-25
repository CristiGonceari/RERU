using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetModuleRolesForSelect {
    public class GetModuleRoleSelectItemsQuery : IRequest<IEnumerable<SelectItem>> {
        public GetModuleRoleSelectItemsQuery (int id) {
            ModuleId = id;
        }
        public int ModuleId { set; get; }
    }
}