using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.KinshipRelations.GetUserProfileKinshipRelations
{
    public class GetUserProfileKinshipRelationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<KinshipRelationDto>>
    {
        public int ContractorId { get; set; }
    }
}
