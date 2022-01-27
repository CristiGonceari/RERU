using CODWER.RERU.Personal.DataTransferObjects.Vacations;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.ApproveRejectRequest
{
    public class ApproveRejectRequestCommand : IRequest<Unit>
    {
        public ApproveRejectRequestCommand(ApproveRejectRequestDto data)
        {
            Data = data;
        }

        public ApproveRejectRequestDto Data { get; set; }
    }
}
