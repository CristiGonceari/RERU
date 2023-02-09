using CODWER.RERU.Evaluation360.Application.Permissions;
using CODWER.RERU.Evaluation360.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.EditArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class EditArticleCommand : IRequest<int>
    {
        public EditArticleEv360Dto Data { get; set; }
    }
}
