using CODWER.RERU.Personal.DataTransferObjects.KinshipRelation;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.KinshipRelations.GetContractorKinshipRelations
{
    public class GetContractorKinshipRelationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<KinshipRelationDto>>
    {
        public int ContractorId { get; set; }
    }
}
