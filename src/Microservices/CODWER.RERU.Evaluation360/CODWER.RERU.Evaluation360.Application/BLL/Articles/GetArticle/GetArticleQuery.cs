using CODWER.RERU.Evaluation360.Application.Permissions;
using CODWER.RERU.Evaluation360.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.GetArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class GetArticleQuery : IRequest<ArticleEv360Dto>
    {
        public int Id { get; set; }
    }
}
