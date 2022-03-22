using CODWER.RERU.Logging.Application.Permissions;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Logging.Application.Articles.GetArticle
{
    [ModuleOperation(permission: PermissionCodes.ARTICLES_GENERAL_ACCESS)]
    public class GetArticleQuery : IRequest<ArticleDto>
    {
        public int Id { get; set; }
    }
}
