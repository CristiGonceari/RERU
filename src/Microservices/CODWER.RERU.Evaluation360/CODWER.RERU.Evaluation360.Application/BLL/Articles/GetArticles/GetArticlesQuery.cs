using CODWER.RERU.Evaluation360.Application.Permissions;
using CODWER.RERU.Evaluation360.DataTransferObjects.Articles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.GetArticles
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class GetArticlesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ArticleEv360Dto>>
    {
        public string Name { get; set; }
    }
}
