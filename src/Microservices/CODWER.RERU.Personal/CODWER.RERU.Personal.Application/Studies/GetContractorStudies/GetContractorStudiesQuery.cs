using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Studies.GetContractorStudies
{
    public class GetContractorStudiesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<StudyDataDto>>
    {
        public int ContractorId { get; set; }
    }
}
