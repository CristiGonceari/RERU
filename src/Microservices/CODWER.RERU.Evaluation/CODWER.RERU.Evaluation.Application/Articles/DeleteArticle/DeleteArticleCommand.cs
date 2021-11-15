using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;


namespace CODWER.RERU.Evaluation.Application.Articles.DeleteArticle
{
    [ModuleOperation(permission: PermissionCodes.ARTICLES_GENERAL_ACCESS)]
    public class DeleteArticleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

