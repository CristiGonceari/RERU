﻿using CODWER.RERU.Logging.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Logging.Application.Articles.DeleteArticle
{
    [ModuleOperation(permission: PermissionCodes.ARTICLES_GENERAL_ACCESS)]
    public class DeleteArticleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

