using System.Collections.Generic;
using CODWER.RERU.Core.DataTransferObjects;
using CODWER.RERU.Evaluation360.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.StorageService.Models;
using MediatR;

namespace CODWER.RERU.Evaluation360.Application.BLL.Articles.AddArticle
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