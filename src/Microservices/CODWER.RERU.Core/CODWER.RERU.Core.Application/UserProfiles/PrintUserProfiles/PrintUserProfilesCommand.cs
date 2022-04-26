using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.PrintUserProfiles
{
    [ModuleOperation(permission: PermissionCodes.VIZUALIZAREA_UTILIZATORILOR)]
    public class PrintUserProfilesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Keyword { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }

    }
}
