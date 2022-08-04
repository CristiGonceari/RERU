using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.FamilyMembers.GetFamilyMembers
{
    public class GetFamilyMembersQueryHandler : IRequestHandler<GetFamilyMembersQuery, PaginatedModel<FamilyMemberDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetFamilyMembersQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<FamilyMemberDto>> Handle(GetFamilyMembersQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.FamilyMembers
                .Include(x => x.Relation)
                .AsQueryable();

            if (request.ContractorId != null)
            {
                items = items.Where(x => x.ContractorId == request.ContractorId);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<FamilyMember, FamilyMemberDto>(items, request);

            return paginatedModel;
        }
    }
}