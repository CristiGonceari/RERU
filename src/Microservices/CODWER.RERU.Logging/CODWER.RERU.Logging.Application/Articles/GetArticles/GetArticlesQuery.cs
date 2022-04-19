using CODWER.RERU.Logging.Application.Permissions;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Logging.Application.Articles.GetArticles
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class GetArticlesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ArticleDto>>
    {
        public string Name { get; set; }
    }
}
