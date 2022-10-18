using System.Collections.Generic;
using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Core.Application.Articles.AddArticle
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ARTICOLE)]
    public class AddArticleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public AddFileDto FileDto { get; set; }
        public List<AssignTagsValuesDto> Roles { get; set; }
    }
}
