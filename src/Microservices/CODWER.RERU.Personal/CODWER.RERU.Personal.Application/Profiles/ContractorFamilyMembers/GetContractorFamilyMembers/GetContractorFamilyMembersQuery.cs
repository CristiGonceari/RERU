using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFamilyMembers.GetContractorFamilyMembers
{
    public class GetContractorFamilyMembersQuery : PaginatedQueryParameter, IRequest<PaginatedModel<FamilyMemberDto>>
    {
    }
}
