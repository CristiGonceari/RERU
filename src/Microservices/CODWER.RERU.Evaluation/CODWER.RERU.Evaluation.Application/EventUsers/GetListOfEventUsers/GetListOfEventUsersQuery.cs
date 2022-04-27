using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventUsers.GetListOfEventUsers
{
    public class GetListOfEventUsersQuery : IRequest<List<int>>
    {
        public int EventId { get; set; }
    }
}
