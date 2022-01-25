using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorsSelectValues
{
    class GetContractorsSelectValuesQueryHandler : IRequestHandler<GetContractorsSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetContractorsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetContractorsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

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
