using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorStudies.GetContractorStudies
{
    public class GetContractorStudiesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<StudyDataDto>>
    {
    }
}
