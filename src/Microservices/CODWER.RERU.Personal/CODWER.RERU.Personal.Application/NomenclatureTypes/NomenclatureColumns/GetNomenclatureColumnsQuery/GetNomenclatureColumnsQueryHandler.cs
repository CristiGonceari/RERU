using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureColumns.GetNomenclatureColumnsQuery
{
    public class GetNomenclatureColumnsQueryHandler: IRequestHandler<GetNomenclatureColumnsQuery, NomenclatureTableHeaderDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNomenclatureColumnsQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<NomenclatureTableHeaderDto> Handle(GetNomenclatureColumnsQuery request, CancellationToken cancellationToken)
        {
            var nomenclatureColumns = _appDbContext.NomenclatureColumns.Where(nc => nc.NomenclatureTypeId == request.NomenclatureTypeId);

            var response = new NomenclatureTableHeaderDto
            {
                NomenclatureTypeId = request.NomenclatureTypeId,
                NomenclatureColumns = nomenclatureColumns.OrderBy(x => x.Order).Select(nc => _mapper.Map<NomenclatureColumnItemDto>(nc)).ToList()
            };

            return response;
        }
    }
}
