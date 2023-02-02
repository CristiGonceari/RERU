using CODWER.RERU.Core.DataTransferObjects.Articles;
using CODWER.RERU.Evaluation360.Application.Permissions;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.GetArticles
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class GetArticlesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ArticleCoreDto>>
    {
        public string Name { get; set; }
    }
}
