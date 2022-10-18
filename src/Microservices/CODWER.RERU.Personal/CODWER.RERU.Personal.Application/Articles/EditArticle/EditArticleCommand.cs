using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Articles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Articles.EditArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ARTICOLE)]
    public class EditArticleCommand : IRequest<int>
    {
        public EditArticlePersonalDto Data { get; set; }
    }
}
