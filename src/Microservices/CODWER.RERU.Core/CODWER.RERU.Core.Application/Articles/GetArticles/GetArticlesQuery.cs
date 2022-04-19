using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Articles.GetArticles
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class GetArticlesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ArticleCoreDto>>
    {
        public string Name { get; set; }
    }
}
