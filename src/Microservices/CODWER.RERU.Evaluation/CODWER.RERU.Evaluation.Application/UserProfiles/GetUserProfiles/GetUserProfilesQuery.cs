﻿using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfiles
{
    public class GetUserProfilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public UserStatusEnum? UserStatusEnum { get; set; }

        public bool EventUsers { get; set; }
        public bool EventResponsiblePerson { get; set; }

        public List<int> ExceptUserIds { get; set; }
    }
}
