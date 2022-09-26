using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Personal.Application.Articles.PrintArticles
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ARTICOLE)]
    public class PrintArticlesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
    }
}
