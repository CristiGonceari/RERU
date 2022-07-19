using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclature
{
    public class GetNomenclatureQueryHandler : IRequestHandler<GetNomenclatureQuery, NomenclatureTypeDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNomenclatureQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<NomenclatureTypeDto> Handle(GetNomenclatureQuery request, CancellationToken cancellationToken)
        {
            var item = await _appDbContext.NomenclatureTypes.FirstAsync(x => x.Id == request.Id);

            return _mapper.Map<NomenclatureTypeDto>(item);
        }
    }
}
