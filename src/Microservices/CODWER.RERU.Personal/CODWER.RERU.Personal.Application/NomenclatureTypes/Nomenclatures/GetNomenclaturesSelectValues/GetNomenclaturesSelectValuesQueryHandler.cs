using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.GetNomenclaturesSelectValues
{
    public class GetNomenclaturesSelectValuesQueryHandler : IRequestHandler<GetNomenclaturesSelectValuesQuery, IEnumerable<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNomenclaturesSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SelectItem>> Handle(GetNomenclaturesSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var nomenclatureTypes = _appDbContext.NomenclatureTypes
                .Where(x=>x.IsActive)
                .OrderBy(x => x.Name)
                .AsEnumerable();

            return nomenclatureTypes.Select(_mapper.Map<SelectItem>);
        }
    }
}
