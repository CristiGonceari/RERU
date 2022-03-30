using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Collections.Generic;


namespace CODWER.RERU.Evaluation.Application.EventUsers.GetEventAssignedUsers
{
    public class GetEventAssignedUsersQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; } 
        public int EventId { get; set; }

    }
}
