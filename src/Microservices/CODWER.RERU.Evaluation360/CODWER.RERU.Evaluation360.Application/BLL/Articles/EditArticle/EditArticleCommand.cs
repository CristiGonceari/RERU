using CODWER.RERU.Core.DataTransferObjects.Articles;
using CODWER.RERU.Evaluation360.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.EditArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class EditArticleCommand : IRequest<int>
    {
        public EditArticleCoreDto Data { get; set; }
    }
}
