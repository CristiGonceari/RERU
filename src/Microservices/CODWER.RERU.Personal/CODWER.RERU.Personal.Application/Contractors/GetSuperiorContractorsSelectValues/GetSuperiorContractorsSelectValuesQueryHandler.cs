using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetSuperiorContractorsSelectValues
{
    public class GetSuperiorContractorsSelectValuesQueryHandler : IRequestHandler<GetSuperiorContractorsSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly List<int> _inferiors;

        public GetSuperiorContractorsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _inferiors = new List<int>();
        }

        public async Task<List<SelectItem>> Handle(GetSuperiorContractorsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            _inferiors.Add(request.ContractorId);
            await GetInferiors(request.ContractorId);

            var items = _appDbContext.Contractors 
                .Where(c => !_inferiors.Contains(c.Id))                             // check parent - inferior            
                .Where(c => c.Positions.Any(p =>                             // only active contractors
                        (p.FromDate == null && p.ToDate == null)
                        || (p.ToDate == null && p.FromDate != null && p.FromDate < now)
                        || (p.FromDate == null && p.ToDate != null && p.ToDate > now)
                        || (p.FromDate != null && p.ToDate != null && p.FromDate < now && p.ToDate > now)))
                .AsQueryable();

            return items.Select(x => _mapper.Map<SelectItem>(x)).ToList();
        }

        private async Task GetInferiors(int contractorId)
        {
            foreach (var contract in _appDbContext.Contracts.Where(x => x.SuperiorId == contractorId).ToList())
            {
                _inferiors.Add((int)contract.SuperiorId);
                GetInferiors((int)contract.SuperiorId);
            }
        }
    }
}