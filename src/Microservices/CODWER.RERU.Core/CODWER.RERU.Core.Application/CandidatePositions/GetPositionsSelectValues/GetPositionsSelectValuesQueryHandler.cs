using AutoMapper;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetPositionsSelectValues
{
    public class GetPositionsSelectValuesQueryHandler : IRequestHandler<GetPositionsSelectValuesQuery, List<SelectItem>>
    {
        private readonly CoreDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetPositionsSelectValuesQueryHandler(CoreDbContext appDbContext, IMapper mapper)
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
