﻿using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.GetUserProfile
{
    [ModuleOperation(permission: Permissions.VIEW_USER_DETAILS)]

    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
        public GetUserProfileQuery(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
