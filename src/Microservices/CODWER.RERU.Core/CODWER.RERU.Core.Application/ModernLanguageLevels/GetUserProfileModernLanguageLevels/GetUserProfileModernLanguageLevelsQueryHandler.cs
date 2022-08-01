using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels.GetUserProfileModernLanguageLevels
{
    public class GetUserProfileModernLanguageLevelsQueryHandler : IRequestHandler<GetUserProfileModernLanguageLevelsQuery, PaginatedModel<ModernLanguageLevelDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserProfileModernLanguageLevelsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }
        public async Task<PaginatedModel<ModernLanguageLevelDto>> Handle(GetUserProfileModernLanguageLevelsQuery request, CancellationToken cancellationToken)
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
