using CODWER.RERU.Logging.Application.Permissions;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Logging.Application.Articles.AddEditArticle
{
    [ModuleOperation(permission: PermissionCodes.ARTICLES_GENERAL_ACCESS)]
    public class AddEditArticleCommand : IRequest<int>
    {
        public ArticleDto Data { get; set; }
    }
}
