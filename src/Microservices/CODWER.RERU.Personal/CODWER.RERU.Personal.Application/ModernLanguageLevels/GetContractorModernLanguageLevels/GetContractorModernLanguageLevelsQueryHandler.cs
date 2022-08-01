using CODWER.RERU.Personal.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels.GetContractorModernLanguageLevels
{
    public class GetContractorModernLanguageLevelsQueryHandler : IRequestHandler<GetContractorModernLanguageLevelsQuery, PaginatedModel<ModernLanguageLevelDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetContractorModernLanguageLevelsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }
        public async Task<PaginatedModel<ModernLanguageLevelDto>> Handle(GetContractorModernLanguageLevelsQuery request, CancellationToken cancellationToken)
        {
            var modernLanguage = _appDbContext.ModernLanguageLevels
                                                .Include(mll => mll.ModernLanguage)
                                                .Where(mll => mll.ContractorId == request.ContractorId)
                                                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<ModernLanguageLevel, ModernLanguageLevelDto>(modernLanguage, request);

            return paginatedModel;
        }
    }
}
