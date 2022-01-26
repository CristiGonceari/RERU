using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.SubordinateRequests.GetRequests
{
    public class GetSubordinateRequestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<DismissalRequestDto>>
    {
    }
}
