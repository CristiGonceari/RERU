using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Articles.AddArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class AddArticleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public AddFileDto FileDto { get; set; }
    }
}
