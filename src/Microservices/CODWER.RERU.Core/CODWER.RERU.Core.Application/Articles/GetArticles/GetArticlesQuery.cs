using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Articles.GetArticles
{
    //[ModuleOperation(permission: PermissionCodes.ARTICLES_GENERAL_ACCESS)]
    public class GetArticlesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ArticleDto>>
    {
        public string Name { get; set; }
    }
}
