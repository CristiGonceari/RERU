using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Articles.DeleteArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ARTICOLE)]
    public class DeleteArticleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

