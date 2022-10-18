using System.Collections.Generic;
using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Personal.Application.Articles.AddArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_ARTICOLE)]
    public class AddArticleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public AddFileDto FileDto { get; set; }
        public List<AssignTagsValuesDto> Roles { get; set; }
    }
}
