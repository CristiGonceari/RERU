using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetNotificatedUsers
{
    public class GetNotificatedUsersQuery : IRequest<List<UserProfileDto>>
    {
        public int CandidatePositionId { get; set; }
    }
}
