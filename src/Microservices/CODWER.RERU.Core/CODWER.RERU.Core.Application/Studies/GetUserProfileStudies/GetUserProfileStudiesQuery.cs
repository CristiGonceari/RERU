using CODWER.RERU.Core.DataTransferObjects.Studies;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.Studies.GetUserProfileStudies
{
    public class GetUserProfileStudiesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<StudyDto>>
    {
        public int ContractorId { get; set; }
    }
}
