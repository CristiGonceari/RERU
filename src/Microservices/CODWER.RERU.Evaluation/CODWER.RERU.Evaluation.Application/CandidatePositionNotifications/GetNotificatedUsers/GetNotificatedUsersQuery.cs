using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetNotificatedUsers
{
    public class GetNotificatedUsersQuery : IRequest<List<SelectItem>>
    {
        public int CandidatePositionId { get; set; }
    }
}
