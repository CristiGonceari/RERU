using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Articles.GetArticle
{
    [ModuleOperation(permission: PermissionCodes.ARTICLES_GENERAL_ACCESS)]
    public class GetArticleQuery : IRequest<ArticleDto>
    {
        public int Id { get; set; }
    }
}
