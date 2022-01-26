using System;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.AddRequest
{
    public class AddDismissalRequestCommand : IRequest<int>
    {
        public DateTime From { get; set; }
    }
}
