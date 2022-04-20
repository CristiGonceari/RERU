using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using RERU.Data.Persistence.Context;
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

        public GetPositionsSelectValuesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<SelectItem>> Handle(GetPositionsSelectValuesQuery request, CancellationToken cancellationToken)
        {
            return _appDbContext.CandidatePositions.AsQueryable()
                .Where(tt => tt.IsActive).Select(tt => _mapper.Map<SelectItem>(tt))
                .ToList();
        }
    }
}
