using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Articles.GetArticles
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ARTICOLE)]
    public class GetArticlesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ArticleDto>>
    {
        public string Name { get; set; }
    }
}
