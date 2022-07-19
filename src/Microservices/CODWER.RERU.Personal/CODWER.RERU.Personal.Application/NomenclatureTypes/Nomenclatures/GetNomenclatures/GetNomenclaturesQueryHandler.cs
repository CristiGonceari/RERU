using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclatures
{
    public class GetNomenclaturesQueryHandler : IRequestHandler<GetNomenclaturesQuery, PaginatedModel<NomenclatureTypeDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetNomenclaturesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<NomenclatureTypeDto>> Handle(GetNomenclaturesQuery request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.NomenclatureTypes
                .OrderByDescending(nt => nt.IsActive)
                .ThenBy(x => x.Name)
                .AsQueryable();

            if (request.IsActive != null)
            {
                items = request.IsActive == true 
                    ? items.Where(x => x.IsActive) 
                    : items.Where(x => !x.IsActive);
            }
            
            return await _paginationService.MapAndPaginateModelAsync<NomenclatureType, NomenclatureTypeDto>(items, request);
        }
    }
}
