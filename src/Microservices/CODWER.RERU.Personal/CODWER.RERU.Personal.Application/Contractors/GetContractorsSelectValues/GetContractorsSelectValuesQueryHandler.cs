using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CVU.ERP.Common;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorsSelectValues
{
    class GetContractorsSelectValuesQueryHandler : IRequestHandler<GetContractorsSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;

        public GetContractorsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _dateTime = dateTime;
        }

        public async Task<List<SelectItem>> Handle(GetContractorsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var now = _dateTime.Now;

            var items = _appDbContext.Contractors
                .Where(c => c.Positions.Any(p =>
                    (p.FromDate == null && p.ToDate == null)
                    || (p.ToDate == null && p.FromDate != null && p.FromDate < now)
                    || (p.FromDate == null && p.ToDate != null && p.ToDate > now)
                    || (p.FromDate != null && p.ToDate != null && p.FromDate < now && p.ToDate > now)))
                .AsQueryable();

            return items.Select(x => _mapper.Map<SelectItem>(x)).ToList();
        }
    }
}
