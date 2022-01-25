using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Internal.GetContractors
{
    public class GetInternalContractorsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ContractorSelectItem>>
    {
        public string Keyword { get; set; }
    }
}
