using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.FamilyComponents;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFamilyMembers.GetContractorFamilyMembers
{
    public class GetContractorFamilyMemberQueryHandler : IRequestHandler<GetContractorFamilyMembersQuery, PaginatedModel<FamilyMemberDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetContractorFamilyMemberQueryHandler(AppDbContext appDbContext,
            IPaginationService paginationService,
            IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<FamilyMemberDto>> Handle(GetContractorFamilyMembersQuery request, CancellationToken cancellationToken)
        {
            var currentContractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.FamilyMembers
                .Include(x => x.Relation)
                .Where(x => x.ContractorId == currentContractorId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<FamilyMember, FamilyMemberDto>(items, request);

            return paginatedModel;
        }
    }
}
