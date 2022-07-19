using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.GetNomenclatureRecordsSelectValues
{
    public class GetNomenclatureRecordsSelectValuesQueryHandler : IRequestHandler<GetNomenclatureRecordsSelectValuesQuery, IEnumerable<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetNomenclatureRecordsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SelectItem>> Handle(GetNomenclatureRecordsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var records = _appDbContext.NomenclatureRecords
                .Include(x => x.NomenclatureType)
                .Where(x => x.IsActive)
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (request.NomenclatureTypeId != null)
            {
                records = records.Where(x => x.NomenclatureTypeId == request.NomenclatureTypeId.Value);
            }

            if (request.NomenclatureBaseType != null)
            {
                records = records.Where(x => x.NomenclatureType.BaseNomenclature == request.NomenclatureBaseType);
            }

            return records.AsEnumerable().Select(_mapper.Map<SelectItem>);
        }
    }
}
