using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Articles.AddEditArticle
{
    [ModuleOperation(permission: PermissionCodes.ARTICLES_GENERAL_ACCESS)]
    public class AddEditArticleCommand : IRequest<int>
    {
        public ArticleCoreDto Data { get; set; }
    }
}
