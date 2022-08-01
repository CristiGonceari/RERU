using CODWER.RERU.Personal.DataTransferObjects.MilitaryObligation;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.MilitaryObligations.GetContractorMilitaryObligations
{
    public class GetContractorMilitaryObligationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<MilitaryObligationDto>>
    {
        public int ContractorId { get; set; }
    }
}
