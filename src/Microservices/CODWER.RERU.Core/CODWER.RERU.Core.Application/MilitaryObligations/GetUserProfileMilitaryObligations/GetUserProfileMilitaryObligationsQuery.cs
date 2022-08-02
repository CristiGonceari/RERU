using CODWER.RERU.Core.DataTransferObjects.MilitaryObligation;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.MilitaryObligations.GetUserProfileMilitaryObligations
{
    public class GetUserProfileMilitaryObligationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<MilitaryObligationDto>>
    {
        public int ContractorId { get; set; }
    }
}
