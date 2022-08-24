using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionsSelectValues
{
    public class GetPositionsSelectValuesQueryHandler : IRequestHandler<GetPositionsSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private List<SelectItem> _list = new ();

        public GetPositionsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetPositionsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                return await GetCandidatePositionsSelectValues();
            }

            var item = await GetSolicitedVacantPosition(request);

            _list.Add(_mapper.Map<SelectItem>(item));

            return _list;
        }

        private async Task<List<SelectItem>> GetCandidatePositionsSelectValues() =>  
               _appDbContext.CandidatePositions.AsQueryable()
                .Where(x => x.From.Value <= DateTime.Now && x.To.Value >= DateTime.Now && x.IsActive)
                .Select(tt => _mapper.Map<SelectItem>(tt))
                .ToList();
        
        private async Task<SolicitedVacantPosition> GetSolicitedVacantPosition(GetPositionsSelectValuesQuery request) =>
              _appDbContext.SolicitedVacantPositions
                .Include(c => c.CandidatePosition)
                .FirstOrDefault(x => x.Id == request.Id);
    }
}
