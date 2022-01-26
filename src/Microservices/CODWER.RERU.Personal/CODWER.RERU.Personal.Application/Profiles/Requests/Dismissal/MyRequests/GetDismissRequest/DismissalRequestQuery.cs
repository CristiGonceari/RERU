using CODWER.RERU.Personal.DataTransferObjects.DismissalRequests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.Requests.Dismissal.MyRequests.GetDismissRequest
{
    public class DismissalRequestQuery : PaginatedQueryParameter, IRequest<PaginatedModel<MyDismissalRequestDto>>
    {
    }
}
