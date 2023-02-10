using CODWER.RERU.Evaluation360.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.DeleteArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class DeleteArticleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}