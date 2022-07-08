using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionsSelectValues
{
    public class GetPositionsSelectValuesQueryHandler : IRequestHandler<GetPositionsSelectValuesQuery, List<SelectItem>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetPositionsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetPositionsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            if (request.Id != null)
            {
                var item = _appDbContext.SolicitedVacantPositions
                    .Include(c => c.CandidatePosition)
                    .FirstOrDefault(x => x.Id == request.Id);

                return new List<SelectItem>
                {
                    new SelectItem
                    {
                        Label = item.CandidatePosition.Name,
                        Value = item.CandidatePositionId.ToString()
                    }
                };
            }

            return _appDbContext.CandidatePositions.AsQueryable()
                .Where(tt => tt.IsActive).Select(tt => _mapper.Map<SelectItem>(tt))
                .ToList();
        }
    }
}
